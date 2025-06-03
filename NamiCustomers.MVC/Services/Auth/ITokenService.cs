using Microsoft.AspNetCore.Http;
using NuGet.Common;

namespace NamiCustomers.MVC.Services.Auth
{
    public interface ITokenService
    {
        string? GetAuthToken();
        string? GetRefreshToken();

        void SetToken(string authToken, string refreshToken);
        void ClearToken();
    }

    public class TokenService(IHttpContextAccessor httpContextAccessor) : ITokenService
    {
        public void ClearToken()
        {
            var httpContext = httpContextAccessor.HttpContext;
            httpContext.Session.Remove("authToken");
            
        }

        public string? GetAuthToken()
        {
            var httpContext = httpContextAccessor.HttpContext;
            return httpContext.Session.GetString("authToken");
        }

        public string? GetRefreshToken()
        {
            var httpContext = httpContextAccessor.HttpContext;
            return httpContext.Session.GetString("refreshToken");
        }

        public void SetToken(string authToken, string refreshToken)
        {
            var httpContext = httpContextAccessor.HttpContext;
            httpContext.Session.SetString("authToken", authToken);
            httpContext.Session.SetString("refreshToken", refreshToken);
        }
    }
}
