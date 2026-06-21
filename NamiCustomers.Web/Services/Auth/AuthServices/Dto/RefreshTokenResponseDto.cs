namespace NamiCustomers.Web.Services.Auth.AuthServices.Dto
{
    public class RefreshTokenResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
