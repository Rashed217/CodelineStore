using CodelineStore.Data.Model;
using JWTAuth.Model;

namespace CodelineStore.Services
{
    public interface IAuthService
    {
        JwtTokenResponse GenerateToken(User user);
        int? GetLoggedInSellerId();
        Task Logout();
        Task SaveTokenToCookie(string token);
        
    }
}