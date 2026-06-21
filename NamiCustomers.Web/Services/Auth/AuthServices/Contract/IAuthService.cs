using NamiCustomers.Web.Services.Auth.AuthServices.Dto;
using NamiCustomers.Web.Services.Common.Dto;

namespace NamiCustomers.Web.Services.Auth.AuthServices.Contract
{
    public interface IAuthService
    {
        Task<ResultDto<SubscriberCodeDto>> LoginAsync(LoginRequestDto loginRequest);
        Task<ResultDto<LoginResponseDto>> ConfirmOtpAsync(string otp);
        Task<string?> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync();

    }
}
