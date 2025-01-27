using Azure;
using JWTAuth.Model;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using CodelineStore.Data.Model;
using CodelineStore.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Serilog;
using Microsoft.EntityFrameworkCore;

namespace CodelineStore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtSettings _jwtSettings;
        private readonly IJSRuntime _jsRuntime;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ApplicationDbContext _context;
        public AuthService(JwtSettings jwtSettings, IJSRuntime jsRuntime, AuthenticationStateProvider authenticationStateProvider, IHttpContextAccessor httpContextAccessor)
        {
            _jwtSettings = jwtSettings;
            _jsRuntime = jsRuntime;
            _authenticationStateProvider = authenticationStateProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public JwtTokenResponse GenerateToken(User user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            SaveTokenToCookie(tokenString);
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(tokenString);
            return new JwtTokenResponse
            {
                Token = tokenString,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes)
            };
        }


        public async Task SaveTokenToCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,    // Prevents client-side access to the cookie (reduces XSS risk)
                Secure = true,      // Set to true in production (to ensure the cookie is only sent over HTTPS)
                Expires = DateTime.UtcNow.AddHours(1),  // Adjust expiry based on your needs
                SameSite = SameSiteMode.Strict // Protect against CSRF attacks
            };

            // Store the token in a cookie
            await _jsRuntime.InvokeVoidAsync("eval", $"document.cookie = 'authToken={token}; {cookieOptions.ToString()}';");
        }

        public int? GetLoggedInSellerId()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                Log.Error("HttpContext is null. Cannot retrieve seller ID.");
                throw new InvalidOperationException("HttpContext is not available.");
            }

            // Retrieve the User ID from claims (assuming NameIdentifier is the user ID)
            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                Log.Error("NameIdentifier claim is missing in the token.");
                return null;
            }

            if (!int.TryParse(userIdClaim.Value, out var userId))
            {
                Log.Error("Failed to parse UserId claim value.");
                return null;
            }

            Log.Information($"UserId claim retrieved: {userId}");

            // Query the database to get the Seller ID associated with the User ID
            var seller = _context.Sellers.FirstOrDefault(s => s.UserId == userId);
            if (seller == null)
            {
                Log.Error($"No seller found for UserId: {userId}");
                return null;
            }

            Log.Information($"SellerId retrieved: {seller.User.Seller.SId}");
            return seller.User.Seller.SId;
        }
    }
}
