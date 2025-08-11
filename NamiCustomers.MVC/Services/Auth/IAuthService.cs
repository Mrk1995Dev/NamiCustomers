using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;
using System.Text.Json;

namespace NamiCustomers.MVC.Services.Auth;
public interface IAuthService
{
    Task<bool> LoginAsync(LoginRequestDto loginRequest);
    Task<string?> RefreshTokenAsync(string refreshToken);
    Task LogoutAsync();
    Task<string> GetOtpAsync(string mobile, string nationalCode);
    Task<ResultDto<LoginResponseDto>> LoginByOtpAsync(string otp);
    Task<ResultDto<ForgotPasswordResponse>> ForgotPasswordAsync(ForgotPasswordRequestDto forgotPasswordRequestDto);
    Task<ResultDto<IdentityResult>> ResetPasswordAsync(ResetPasswordDto reset);
    Task<ResultDto> RegisterAsync(RegisterUserDto registerDto);
    Task<ConfirmResponse> ConfirmEmailAsync(string userId, string token);
    Task<HttpResponseMessage> SetPhoneNumberAsync(SetPhoneNumberDto phoneNumberDto);
    Task<ConfirmResponse> VerifyPhoneNumberAsync(VerifyPhoneNumberDto verify);
}
public class AuthService(HttpClient httpClient, ITokenSessionService tokenService
        , AuthenticationStateProvider authenticationStateProvider) : IAuthService
{


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
    public async Task<ConfirmResponse> ConfirmEmailAsync(string userId, string token)
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

    public async Task<ConfirmResponse> ConfirmMobileAsync(string userId, string token)
    {
        var response = await httpClient.PostAsJsonAsync("Account/ConfirmMobile", new ConfirmRequest { UserId = userId, Token = token });
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



    public async Task<ResultDto> RegisterAsync(RegisterUserDto registerDto)
    {
        var response = await httpClient.PostAsJsonAsync("Account/Register", registerDto);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<ResultDto>();
            if (result != null)
            {
                return result;
            }
        }
        return new ResultDto(Infrastucture.Properties.Resources.errSave, false);
    }


    public async Task<string> GetOtpAsync(string mobile, string nationalCode)
    {
        var response = await GetData<ResultDto<SubscriberCodeDto>>($"Account/GetOtp?mobile={mobile}&nationalCode=", nationalCode);

        if (response.Succeeded)
        {
            return response.Data.AuthCode;
        }
        return Infrastucture.Properties.Resources.errGetOtp;
    }

    public async Task<ResultDto<LoginResponseDto>> LoginByOtpAsync(string otp)
    {

        var result = await GetData<ResultDto<LoginResponseDto>>("Account/LoginByOtp?otpCode=", otp);


        if (result.Succeeded)
        {

            //ذخیره اطلاعات
            tokenService.SetToken(result.Data.Token, result.Data.RefreshToken);

            //اطلاع رسانی تغییر وضعیت کاربر

            ((CustomAuthenticationStateProvider)authenticationStateProvider).UpdateAuthenticationState();
            return result;
        }
        return result;
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

    public async Task<ResultDto<ForgotPasswordResponse>> ForgotPasswordAsync(ForgotPasswordRequestDto forgotPasswordRequestDto)
    {
        var result = await PostData<ResultDto<ForgotPasswordResponse>>("Account/ForgotPassword", forgotPasswordRequestDto);
        return result;
    }

    public async Task<ResultDto<IdentityResult>> ResetPasswordAsync(ResetPasswordDto reset)
    {
        var result = await PostData<ResultDto<IdentityResult>>("Account/ResetPassword", reset);
        return result;
    }

    public async Task<HttpResponseMessage> SetPhoneNumberAsync(SetPhoneNumberDto phoneNumberDto)
    {
        var result = await PostData<HttpResponseMessage>("Account/SetPhoneNumber", phoneNumberDto);
        return result;
    }

    public async Task<ConfirmResponse> VerifyPhoneNumberAsync(VerifyPhoneNumberDto verify)
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


