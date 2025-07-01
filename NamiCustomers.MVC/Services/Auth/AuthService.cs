using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

using NamiCustomers.Infrastucture.Model;
using NamiCustomers.Infrastucture.Model.Subscribers;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Text.Json;


namespace NamiCustomers.MVC.Services.Auth
{

    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginRequestDto loginRequest);
        Task<string?> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync();
        Task<string> GetOtp(string mobile, string nationalCode);
        Task<LoginResponseDto> LoginByOtpAsync(string otp);
        Task<ResultDto<ForgotPasswordResponse>> ForgotPassword(ForgotPasswordRequestDto forgotPasswordRequestDto);
        Task<ResultDto<IdentityResult>> ResetPassword(ResetPasswordDto reset);
        Task<RegisterResponse> Register(RegisterDto register);
        Task<ConfirmResponse> ConfirmEmail(string userId, string token);
        Task<HttpResponseMessage> SetPhoneNumber(SetPhoneNumberDto phoneNumberDto);
        Task<ConfirmResponse> VerifyPhoneNumber(VerifyPhoneNumberDto verify);
    }
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


        #region Privates
        private async Task<T> GetData<T>(string apiAddress, dynamic queryString)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{apiAddress}{queryString}");

            var token = tokenService.GetAuthToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            var a = JsonSerializer.Deserialize<T>(content);

            return await Task.FromResult(a);
        }

        private async Task<T> PostData<T>(string apiAddress, dynamic queryModel)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{apiAddress}");

            var token = tokenService.GetAuthToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonSerializer.Serialize(queryModel), null, "application/json");
            request.Content = content;
            var response = await httpClient.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();
            return await Task.FromResult(JsonSerializer.Deserialize<T>(responseContent));
        }







        #endregion
        public async Task<ConfirmResponse> ConfirmEmail(string userId, string token)
        {
            var response = await httpClient.PostAsJsonAsync("Account/ConfirmEmail", new ConfirmRequest { UserId = userId, Token = token });
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ConfirmResponse>();
                if (result != null)
                {
                    return result;
                }
            }
            return new ConfirmResponse { IsSuccess = false };
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



        public async Task<RegisterResponse> Register(RegisterDto registerDto)
        {
            var response = await httpClient.PostAsJsonAsync("Account/Register", registerDto);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();
                if (result != null)
                {
                    return result;
                }
            }
            return new RegisterResponse { IsSuccess = false };
        }


        public async Task<string> GetOtp(string mobile, string nationalCode)
        {
            var response = await GetData<ResultDto<SubscriberCodeDto>>($"Account/GetOtp?mobile={mobile}&nationalCode=", nationalCode);
            return response.Data.AuthCode;
        }

        public async Task<LoginResponseDto> LoginByOtpAsync(string otp)
        {

            var result = await GetData<ResultDto<LoginResponseDto>>("Account/LoginByOtp?otpCode=", otp);


            if (result.Issuccess)
            {

                //ذخیره اطلاعات
                tokenService.SetToken(result.Data.Token, result.Data.RefreshToken);

                //اطلاع رسانی تغییر وضعیت کاربر

                ((CustomAuthenticationStateProvider)authenticationStateProvider).UpdateAuthenticationState();
                return result.Data;
            }
            return new LoginResponseDto() { };//TODO 
        }

        public async Task LogoutAsync()
        {
            var refreshToken = tokenService.GetRefreshToken();
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new InvalidOperationException("Refresh token not found.");
            }

            var response = await httpClient.PostAsJsonAsync("Account/logout", new { RefreshToken = refreshToken });
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

            var response = await httpClient.PostAsJsonAsync("Account/refresh", new { RefreshToken = refreshToken });

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

        public async Task<ResultDto<ForgotPasswordResponse>> ForgotPassword(ForgotPasswordRequestDto forgotPasswordRequestDto)
        {
            var result = await PostData<ResultDto<ForgotPasswordResponse>>("Account/ForgotPassword", forgotPasswordRequestDto);
            return result;
        }

        public async Task<ResultDto<IdentityResult>> ResetPassword(ResetPasswordDto reset)
        {
            var result = await PostData<ResultDto<IdentityResult>>("Account/ResetPassword", reset);
            return result;
        }

        public async Task<HttpResponseMessage> SetPhoneNumber(SetPhoneNumberDto phoneNumberDto)
        {
            var result = await PostData<HttpResponseMessage>("Account/SetPhoneNumber", phoneNumberDto);
            return result;
        }

        public async Task<ConfirmResponse> VerifyPhoneNumber(VerifyPhoneNumberDto verify)
        {
            var response = await httpClient.PostAsJsonAsync("Account/VerifyPhoneNumber", verify);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ConfirmResponse>();
                if (result != null)
                {
                    return result;
                }
            }
            return new ConfirmResponse { IsSuccess = false };
        }
    }
}


