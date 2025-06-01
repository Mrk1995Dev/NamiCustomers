using NamiCustomers.Web.Services.LocalStorage;

namespace NamiCustomers.Web.Services.Auth.TokenServices
{
    public interface ITokenService
    {
        Task<string?> GetAuthTokenAsync();
        Task<string?> GetRefreshTokenAsync();

        Task SetTokenAsync(string authToken, string refreshToken);
        Task ClearTokenAsync();
    }

    public class TokenService : ITokenService
    {
        private readonly ILocalStorageService _localStorage;

        public TokenService(ILocalStorageService localStorageService)
        {
            _localStorage = localStorageService;
        }

        public async Task ClearTokenAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("refreshToken");
        }

        public async Task<string?> GetAuthTokenAsync()
        {
            return await _localStorage.GetItemAsync("authToken");
        }

        public async Task<string?> GetRefreshTokenAsync()
        {
            return await _localStorage.GetItemAsync("refreshToken");
        }

        public async Task SetTokenAsync(string authToken, string refreshToken)
        {
            await _localStorage.SetItemAsync("authToken", authToken);
            await _localStorage.SetItemAsync("refreshToken", refreshToken);
        }
    }
}
