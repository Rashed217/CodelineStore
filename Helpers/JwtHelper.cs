using System.IdentityModel.Tokens.Jwt;

namespace CodelineStore.Helpers
{
    public class JwtHelper
    {
        public static string GetClaimValue(string jwtToken, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();
            if (handler.CanReadToken(jwtToken))
            {
                var jwtTokenObj = handler.ReadJwtToken(jwtToken);
                var claim = jwtTokenObj.Claims.FirstOrDefault(c => c.Type == claimType);
                return claim?.Value ?? throw new ArgumentException($"Claim '{claimType}' not found in the token.");
            }
            throw new ArgumentException("Invalid JWT Token.");
        }
    }
}
