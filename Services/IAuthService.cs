using CodelineStore.Data.Model;
using JWTAuth.Model;

namespace CodelineStore.Services
{
    public interface IAuthService
    {
        JwtTokenResponse GenerateToken(User user);
        int? GetLoggedInSellerId();
        Task SaveTokenToCookie(string token);
    }
}