using Microsoft.AspNetCore.Http;
using System;

namespace CodelineStore.Services
{
    public class CookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetCookie(string key, string value, int? expireTimeMinutes, bool httpOnly = true)
        {
            var options = new CookieOptions
            {
                HttpOnly = httpOnly,
                Secure = true, // Ensure it's only sent over HTTPS
                SameSite = SameSiteMode.Strict, // Prevent CSRF
                Expires = expireTimeMinutes.HasValue
                    ? DateTime.UtcNow.AddMinutes(expireTimeMinutes.Value)
                    : (DateTimeOffset?)null
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append(key, value, options);
        }

        public string? GetCookie(string key)
        {
            string? value = null; // Initialize the variable to null
            _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(key, out value);
            return value;
        }

        public void RemoveCookie(string key)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(key);
        }
    }
}
