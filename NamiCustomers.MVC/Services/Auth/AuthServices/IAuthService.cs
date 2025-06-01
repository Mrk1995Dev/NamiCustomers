using NamiCustomers.MVC.Services.Auth.Dtos;

namespace NamiCustomers.MVC.Services.Auth.AuthServices
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginRequestDto loginRequest);
        Task<string?> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync();

    }
}
