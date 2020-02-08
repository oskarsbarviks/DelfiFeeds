using FrontendLogic.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FrontendLogic
{
    /// <summary>
    /// Authorize user, adds claims and creates a cookie
    /// </summary>
    public class AuthorizationLogic : IAuthorizationLogic
    {
        public async Task AuthorizeUser(string id, string name, string email, HttpContext httpContext)
        {
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, name),
                 new Claim(ClaimTypes.Email, email),
                 new Claim(ClaimTypes.NameIdentifier, id),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                IsPersistent = true,
                IssuedUtc = DateTime.UtcNow
            };

            await httpContext.SignInAsync
                (
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );
        }
    }
}
