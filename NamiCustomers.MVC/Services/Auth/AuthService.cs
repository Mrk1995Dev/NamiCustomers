using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Infrastucture.Model;
using NamiCustomers.Infrastucture.Model.Subscribers;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;


namespace NamiCustomers.MVC.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginRequestDto loginRequest);
        Task<string?> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync();
        Task<string> GetOtp(string mobile);
        Task<LoginResponseDto> LoginByOtpAsync(string otp);
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

        public async Task<string> GetOtp(string mobile)
        {
            var response = await GetData<ResultDto<SubscriberCodeDto>>("Account/GetOtp?mobile=", mobile);
            return response.Data.AuthCode;
        }

        public async Task<LoginResponseDto> LoginByOtpAsync(string otp)
        {

            var result = await GetData<ResultDto<LoginResponseDto>>("Account/LoginByOtp?otpCode=", otp);


            if (result != null)
            {

                //ذخیره اطلاعات
                tokenService.SetToken(result.Data.Token, result.Data.RefreshToken);

                //اطلاع رسانی تغییر وضعیت کاربر

                ((CustomAuthenticationStateProvider)authenticationStateProvider).UpdateAuthenticationState();
                return result.Data;
            }

            return new LoginResponseDto ();//TODO 
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
    }
}



public class Rootobject
{
    public bool issuccess { get; set; }
    public string message { get; set; }
    public Datum[] Data { get; set; }
}

public class Datum
{
    public string AuthenticationType { get; set; }
    public bool IsAuthenticated { get; set; }
    public object Actor { get; set; }
    public object BootstrapContext { get; set; }
    public Claim[] Claims { get; set; }
    public object Label { get; set; }
    public string Name { get; set; }
    public string NameClaimType { get; set; }
    public string RoleClaimType { get; set; }
}

public class Claim
{
    public string Issuer { get; set; }
    public string OriginalIssuer { get; set; }
    public Properties Properties { get; set; }
    public object Subject { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
    public string ValueType { get; set; }
}

public class Properties
{
    public string httpschemasxmlsoaporgws200505identityclaimpropertiesShortTypeName { get; set; }
}
