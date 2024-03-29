﻿using Eum.Core.Shared.Domains;
using Eum.Core.Shared.Models;
using Eum.Core.Shared.Repositories;
using Eum.Core.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Eum.Core.Shared.Services
{
    public interface ITokenService : IService
    {
        Task<IToken> CreateAsync(string username, int? expires = null);
        Task<string> CreateEumCookieValue(string inputUserName);
        Task<IToken> CreateForServiceAsync(string serviceName, int? expires = null);
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
    public class TokenService : ITokenService
    {
        protected readonly ILogger<TokenService> _logger;
        protected readonly IConfiguration _configuration;
        protected readonly IPersonRepository _personRepository;
        protected readonly IAppConfigRepository _appConfigRepository;
        private readonly IHashService _hashService;
        private readonly IEncryptService _encryptService;
        protected readonly SecurityKey _securityKey;
        protected readonly string _securityAlgorithm;
        protected readonly SigningCredentials _signingCredentials;

        public TokenService(
            ILogger<TokenService> logger,
            IConfiguration configuration,
            IPersonRepository personRepository,
            IHashService hashService,
            IEncryptService encryptService,
            IAppConfigRepository appConfigRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _personRepository = personRepository;
            _appConfigRepository = appConfigRepository;
            _hashService = hashService;
            _encryptService = encryptService;

            // 비밀키 생성
            var secKeyStr = _configuration.GetValue("AppSettings:Jwt:SecretKey", string.Empty);
            if (string.IsNullOrEmpty(secKeyStr)) throw new InvalidOperationException("Can't be value empty or null from AppSettings:Jwt:SecretKey");
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secKeyStr));

            // 키를 Sha512로 암호화
            _signingCredentials = new SigningCredentials(_securityKey, _securityAlgorithm = SecurityAlgorithms.HmacSha256);
        }

        #region Token

        public async Task<string> CreateEumCookieValue(string username)
        {
            var tokenSeparator = _configuration.GetValue("AppSettings:Jwt:TokenSeparator", string.Empty);
            var token = await this.CreateAsync(username);
            var sbCookieValue = new StringBuilder();
            sbCookieValue.Append(token.AccessToken);
            sbCookieValue.Append(tokenSeparator);
            sbCookieValue.Append(token.RefreshToken);
            var szCookieValue = _encryptService.EncryptToString(sbCookieValue.ToString());
            return szCookieValue;
        }

        public virtual Task<IToken> CreateAsync(string username, int? expires = null)
        {
            // 최종 토큰을 만드는 데 사용되는 정보 생성
            var claims = GetClaims(username);
            var expiresMins = expires ?? _appConfigRepository.GetByKey("TokenExpiresMinutes", 720);
            return BuildToken(claims, expiresMins);
        }

        public virtual Task<IToken> CreateForServiceAsync(string serviceName, int? expires = null)
        {
            var tokenSeparator = _configuration.GetValue("AppSettings:Jwt:TokenSeparator", string.Empty);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, serviceName),
                new Claim(JwtRegisteredClaimNames.Jti, $"{serviceName}_{DateTime.UtcNow.Ticks}"),
                new Claim(ClaimTypes.NameIdentifier, serviceName),
                new Claim(ClaimTypes.Name, serviceName),
                new Claim(ClaimNames.TokenSeparator, tokenSeparator ?? "/")
            };
            // 최종 토큰을 만드는 데 사용되는 정보 생성
            var expiresMins = expires ?? 142560 /* default 99년 */;
            return BuildToken(claims, expiresMins);
        }

        private Task<IToken> BuildToken(IEnumerable<Claim> claims, int expires)
        {
            // access token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration.GetValue("AppSettings:Jwt:Issuer", string.Empty),
                Audience = _configuration.GetValue("AppSettings:Jwt:Audience", string.Empty),
                Expires = DateTime.UtcNow.AddMinutes(expires),
                //NotBefore = DateTime.Now,
                SigningCredentials = _signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            return Task.FromResult<IToken>(new JsonWebToken
            {
                AccessToken = accessToken,
                RefreshToken = GetRefreshToken(),
                Expires = tokenDescriptor.Expires.Value,
                Claims = claims
            });
        }

        private string GetRefreshToken()
        {
            return _hashService.GetHashString(Guid.NewGuid().ToString());
        }
        #endregion Token



        #region Claims
        protected virtual IEnumerable<Claim> GetClaims(string username)
        {
            Person? user;
            if (username.Contains("@"))
                user = _personRepository.GetUserInfoByEmail(username).FirstOrDefault();
            else user = _personRepository.GetUserInfoByPersonCode(username).FirstOrDefault();
            if (user is null) throw new Exception($"User does not exist ({username})");

            var tokenSeparator = _configuration.GetValue("AppSettings:Jwt:TokenSeparator", string.Empty);
            string szEmail = user.Email;
            string szPersonCode = user.PersonCode;
            string szPersonId = user.PersonId;
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, szEmail),
                new Claim(JwtRegisteredClaimNames.Jti, $"{szPersonCode}_{DateTime.UtcNow.Ticks}"),
                new Claim(JwtRegisteredClaimNames.Email, szEmail),
                new Claim(ClaimTypes.NameIdentifier, szPersonId),
                new Claim(ClaimTypes.Name, szPersonCode),
                new Claim(ClaimNames.UserID,user.PersonCode),
                new Claim(ClaimNames.UserCode,user.PersonCode),
                new Claim(ClaimNames.UserEmail,szEmail),
                new Claim(ClaimNames.UserName,user.DisplayName ?? string.Empty),
                new Claim(ClaimNames.EmployeeNumber,user.EmployeeNumber?? string.Empty),
                new Claim(ClaimNames.ComCode, user.ComCode?? string.Empty),
                new Claim(ClaimNames.DeptCode, user.DeptCode ?? string.Empty),
                new Claim(ClaimNames.DeptName, user.DeptName ?? string.Empty),
                new Claim(ClaimNames.TitleCode, user.TitleCode ?? string.Empty),
                new Claim(ClaimNames.TitleName, user.TitleName ?? string.Empty),
                new Claim(ClaimNames.TokenSeparator, tokenSeparator)
            };
        }

        public virtual ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secKeyStr = _configuration.GetValue("AppSettings:Jwt:SecretKey", string.Empty);
            if (string.IsNullOrEmpty(secKeyStr)) throw new InvalidOperationException("Can't be value empty or null from AppSettings:Jwt:SecretKey");
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = _configuration.GetValue("AppSettings:Jwt:Issuer", string.Empty),
                    ValidAudience = _configuration.GetValue("AppSettings:Jwt:Audience", string.Empty),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secKeyStr)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false // << important!!!
                };
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return principal;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected virtual bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return validatedToken is JwtSecurityToken jwtSecurityToken &&
                jwtSecurityToken.Header.Alg.Equals(_securityAlgorithm);
        }

        #endregion Claims
    }
}
