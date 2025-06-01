using Microsoft.AspNetCore.Components.Authorization;
using NamiCustomers.MVC.Services.Auth.Dtos;
using NamiCustomers.MVC.Services.Auth.TokenServices;

namespace NamiCustomers.MVC.Services.Auth.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthService(HttpClient httpClient, ITokenService tokenService
            , AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
            this.authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<bool> LoginAsync(LoginRequestDto loginRequest)
        {
            var response = await httpClient.PostAsJsonAsync("Account/LogIn", loginRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                if (result != null)
                {

                    //ذخیره اطلاعات
                     tokenService.SetToken(result.Token, result.RefreshToken);

                    //اطلاع رسانی تغییر وضعیت کاربر

                    ((CustomAuthenticationStateProvider)authenticationStateProvider).UpdateAuthenticationState();
                    return true;
                }
            }

            return false;
        }

        public async Task LogoutAsync()
        {
            var refreshToken = tokenService.GetRefreshToken();
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new InvalidOperationException("Refresh token not found.");
            }

            var response = await httpClient.PostAsJsonAsync("/Account/logout", new { RefreshToken = refreshToken });
            if (response.IsSuccessStatusCode)
            {
                  tokenService.ClearToken();
                ((CustomAuthenticationStateProvider)authenticationStateProvider).UpdateAuthenticationState();

            }
            else
            {
                tokenService.ClearToken();
                ((CustomAuthenticationStateProvider)authenticationStateProvider).UpdateAuthenticationState();
                throw new Exception("Failed to logout.");
            }
        }

        public async Task<string?> RefreshTokenAsync(string refreshToken)
        {

            var response = await httpClient.PostAsJsonAsync("/Account/refresh", new { RefreshToken = refreshToken });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RefreshTokenResponseDto>();
                if (result != null)
                {
                    tokenService.SetToken(result.Token, result.RefreshToken);
                    return result.Token;
                }
            }
            else
            {
                tokenService.ClearToken();
            }
            return null;
        }
    }
}
