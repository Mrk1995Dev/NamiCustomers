using Microsoft.AspNetCore.Components.Authorization;
using NamiCustomers.Web.Services.Auth.TokenServices;
using NamiCustomers.Web.Services.Common.Dto;
using System.Net;
using System.Net.Http.Json;

namespace NamiCustomers.Web.Services.Auth.AuthServices.Implementation
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

        public async Task<ResultDto<SubscriberCodeDto>> LoginAsync(LoginRequestDto loginRequest)
        {
            var result = new ResultDto<SubscriberCodeDto>("", false);
            var response = await httpClient.GetAsync($"Account/GetOtp?nationalCode={loginRequest.NationalCode}&mobile={loginRequest.PhoneNumber}");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<SubscriberCodeDto>>();
                return new ResultDto<SubscriberCodeDto>(result.Message, result.Succeeded);
            }

            else
            {
                return new ResultDto<SubscriberCodeDto>(result.Message, result.Succeeded);
            }
        }

        public async Task<ResultDto<LoginResponseDto>> ConfirmOtpAsync(string otp)
        {
            var result = new ResultDto<LoginResponseDto>("", false);
            var response = await httpClient.GetAsync($"Account/LogInByOtp?otpCode={otp}");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return new ResultDto<LoginResponseDto>("", false);

            else if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<LoginResponseDto>>();
                await tokenService.SetTokenAsync(result.Data.Token, result.Data.RefreshToken);
                ((CustomAuthenticationStateProvider)authenticationStateProvider).UpdateAuthenticationState();
                return new ResultDto<LoginResponseDto>(result.Message, true);
            }

            else if(response.StatusCode == HttpStatusCode.BadRequest)
            {
                //var failure = await response.Content.ReadFromJsonAsync<ResultDto.Failure<LoginResponseDto>("")>>
                var failure = await response.Content.ReadFromJsonAsync<ResultDto<LoginResponseDto>>();
                return new ResultDto<LoginResponseDto>(failure.Message, failure.Succeeded);
            }

            else
            {
                return new ResultDto<LoginResponseDto>(result.Message, result.Succeeded);
            }
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
