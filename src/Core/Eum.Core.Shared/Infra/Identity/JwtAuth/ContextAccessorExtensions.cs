using Eum.Core.Shared.Domains;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Shared.Infra.Identity.JwtAuth
{
    public static class ContextAccessorExtensions
    {
        public static string GetClaim(this ClaimsPrincipal user, string type)
        {
            if (user == null)
                return null;

            var targetClaim = user.Claims.FirstOrDefault(x =>
                string.Compare(type.ToLower(), x.Type.ToLower()) == 0);

            if (targetClaim == null)
                return null;
            else
                return targetClaim.Value;
        }

        public static string Get(this ClaimsPrincipal user, string key)
        {
            return user.GetClaim(key);
        }

        public static string GetPersonCode(this ClaimsPrincipal user)
        {
            return user.GetClaim(ClaimNames.UserCode);
        }

        public static string GetComCode(this ClaimsPrincipal user)
        {
            return user.GetClaim(ClaimNames.ComCode);
        }

        public static string GetDisplayName(this ClaimsPrincipal user)
        {
            return user.GetClaim(ClaimNames.UserName);
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.GetClaim(ClaimNames.UserEmail);
        }

        public static string GetEmployeeNumber(this ClaimsPrincipal user)
        {
            return user.GetClaim(ClaimNames.EmployeeNumber);
        }

        public static string GetClaim(this IHttpContextAccessor accessor, string type)
        {
            var user = accessor.HttpContext.User;
            return user.GetClaim(type);
        }
    }
}
