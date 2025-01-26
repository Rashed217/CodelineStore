using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace JWTAuthentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public CustomAuthenticationStateProvider(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await GetTokenFromCookiesAsync();

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public void MarkUserAsAuthenticated(string token)
        {
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);
            var authState = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public void MarkUserAsLoggedOut()
        {
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            var authState = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        private async Task<string> GetTokenFromCookiesAsync()
        {
            try
            {
                // Retrieve the token from the cookie via JavaScript interop
                var token = await _jsRuntime.InvokeAsync<string>("eval", "document.cookie.split('; ').find(row => row.startsWith('authToken=')).split('=')[1]");
                return token;
            }
            catch (Exception ex) 
            {
                return null;

            }
            
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();

            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs.TryGetValue(ClaimTypes.Name, out object username) || keyValuePairs.TryGetValue("unique_name", out username))
            {
                claims.Add(new Claim(ClaimTypes.Name, username.ToString()));
            }

            if (keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles))
            {
                var roleList = roles.ToString().Split(',');
                foreach (var role in roleList)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }

}
