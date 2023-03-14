using Eum.Core.Shared.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eum.Core.Shared.Infra.Identity.JwtAuth
{
    public static class AuthorizationExtension
    {
        public static bool IsAnonymousAllowed(this HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            return endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is object;
        }

        public static void AddJwtAuth(this IServiceCollection services)
        {
            services.AddTransient<AuthorizationHeaderMiddleware>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            LifetimeValidator lifetimeValidator = (notBefore, expires, securityToken, validationParameter)
                => expires != null ? expires > DateTime.UtcNow : false;
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = "Eum TeamsPlus",
                ValidAudience = "Eum TeamsPlus",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("EAF0FD24-1B70-4550-8BA5-F0507AE9C1A6")),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                LifetimeValidator = lifetimeValidator,
            };
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
                options.SaveToken = true;
            });
        }


        public static void SetCookie(this HttpResponse response, string token)
        {
            var configuration = Static.ServiceProvider.GetService<IConfiguration>();
            var appConfigRepository = Static.ServiceProvider.GetService<IAppConfigRepository>();
            var expiresMins = appConfigRepository!.GetByKey("TokenCookieExpiresMinutes", 1440);
            var bUseSecure = configuration!.GetValue("AppSettings:Jwt:CookieSecure", true);
            var sameSiteMode = (SameSiteMode)Enum.Parse(typeof(SameSiteMode), configuration!.GetValue("AppSettings:Jwt:CookieSameSiteMode", "None")!);
            var cookieName = configuration!.GetValue("AppSettings:Jwt:CookieName", ".Eum.AccessToken");
            var option = new CookieOptions
            {
                HttpOnly = true,
#if DEBUG
                SameSite = SameSiteMode.Strict,
                Secure = false,
#else
                SameSite = sameSiteMode,
                Secure = bUseSecure,
#endif
                Expires = DateTimeOffset.UtcNow.AddMinutes(expiresMins)
            };


            //#if DEBUG
            //                option.Secure = false;
            //#else
            //                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Cookies
            //                // A secure cookie is only sent to the server with an encrypted request over 
            //                // the HTTPS protocol. Even with Secure, sensitive information should never be stored in cookies, 
            //                // as they are inherently insecure and this flag can't offer real protection. Starting with Chrome 52 and 
            //                // Firefox 52, insecure sites (http:) can't set cookies with the Secure directive.
            //                option.Secure = true;
            //#endif
            response.Cookies.Append(cookieName!, token, option);
        }
    }
}
