using NamiCustomers.Web.Models.Auth;

namespace NamiCustomers.Web.Services.Auth.AuthServices
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginRequestDto loginRequest);
        Task<string?> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync();

    }
}
