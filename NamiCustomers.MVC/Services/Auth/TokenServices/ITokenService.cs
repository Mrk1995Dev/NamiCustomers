using Microsoft.AspNetCore.Http;

namespace NamiCustomers.MVC.Services.Auth.TokenServices
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
            httpContext.Response.Cookies.Delete("authToken");
            httpContext.Response.Cookies.Delete("refreshToken");
        }

        public string? GetAuthToken()
        {
            var httpContext = httpContextAccessor.HttpContext;
            return httpContext.Request.Cookies["authToken"];
        }

        public string? GetRefreshToken()
        {
            var httpContext = httpContextAccessor.HttpContext;
            return httpContext.Request.Cookies["refreshToken"];
        }

        public void SetToken(string authToken, string refreshToken)
        {

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // Prevent JavaScript access
                Secure = true,   // Only send over HTTPS
                SameSite = SameSiteMode.Strict, // Prevent CSRF
                Expires = DateTime.UtcNow.AddHours(1) // Set appropriate expiration
            };
            var httpContext = httpContextAccessor.HttpContext;
            httpContext.Response.Cookies.Append("authToken", authToken, cookieOptions);
            httpContext.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);

        }
    }
}
