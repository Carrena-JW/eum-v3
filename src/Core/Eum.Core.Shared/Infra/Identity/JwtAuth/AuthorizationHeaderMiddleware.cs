using Eum.Core.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Shared.Infra.Identity.JwtAuth
{
    public class AuthorizationHeaderMiddleware : IMiddleware
    {
        private IConfiguration _configuration;
        private IEncryptService _encryptService;
        private string? _tokenSeparator;

        public AuthorizationHeaderMiddleware(IConfiguration configuration, IEncryptService encryptService)
        {
            _configuration = configuration;
            _encryptService = encryptService;
            _tokenSeparator = _configuration.GetValue("AppSettings:Jwt:TokenSeparator", string.Empty);
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.IsAnonymousAllowed()) await next(context);
            else
            {
                var cookieName = _configuration.GetValue("AppSettings:Jwt:CookieName", ".Eum.AccessToken");
                var request = context.Request;
                if (request.Cookies.ContainsKey(cookieName) == true &&
                    request.Headers.ContainsKey("Authorization") == false)
                {
                    var szCookieValue = request.Cookies[cookieName];
                    var szDecrypted = _encryptService.DecryptFromString(szCookieValue);
                    var tokens = szDecrypted.Split(_tokenSeparator, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens.Length == 2)
                    {
                        var token = tokens[0];
                        var refreshToken = tokens[1];
                        request.Headers.Append("Authorization", "Bearer " + token);
                    }
                }

                await next(context);
            }
        }
    }
}
