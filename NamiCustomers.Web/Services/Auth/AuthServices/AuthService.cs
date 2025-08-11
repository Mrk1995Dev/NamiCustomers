using Microsoft.AspNetCore.Components.Authorization;
using NamiCustomers.Web.Models.Auth;
using NamiCustomers.Web.Services.Auth.TokenServices;
using System.Net.Http.Json;

namespace NamiCustomers.Web.Services.Auth.AuthServices
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
        //https://localhost:7061/api/v1/Account/GetOtp?mobile=09191646456
        public async Task<bool> LoginAsync(LoginRequestDto loginRequest)
        {
            var response = await httpClient.PostAsJsonAsync("Account/LogIn", loginRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                if (result != null)
                {

                    //ذخیره اطلاعات
                    await tokenService.SetTokenAsync(result.Token, result.RefreshToken);

                    //اطلاع رسانی تغییر وضعیت کاربر

                    ((CustomAuthenticationStateProvider)authenticationStateProvider).UpdateAuthenticationState();
                    return true;
                }
            }

            return false;
        }

        public async Task LogoutAsync()
        {
            var refreshToken = await tokenService.GetRefreshTokenAsync();
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new InvalidOperationException("Refresh token not found.");
            }

            var response = await httpClient.PostAsJsonAsync("/Account/logout", new { RefreshToken = refreshToken });
            if (response.IsSuccessStatusCode)
            {
                await tokenService.ClearTokenAsync();
                ((CustomAuthenticationStateProvider)authenticationStateProvider).UpdateAuthenticationState();

            }
            else
            {
                await tokenService.ClearTokenAsync();
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
                    await tokenService.SetTokenAsync(result.Token, result.RefreshToken);
                    return result.Token;
                }
            }
            else
            {
                await tokenService.ClearTokenAsync();
            }
            return null;
        }
    }
}
