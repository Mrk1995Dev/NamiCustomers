namespace NamiCustomers.Infrastucture.Model.Account
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string Email { get; set; }
        public string NationalCode { get; set; }
        public string Mobile { get; set; }
    }

    public record ForgotPasswordResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }

    public record RegisterResponse
    {
        public List<string> Errors { get; set; }
        public bool IsSuccess { get; set; }
    }
    public record ConfirmResponse
    {
        public List<string> Errors { get; set; }
        public bool IsSuccess { get; set; }
    }

    public record ConfirmRequest
    {
       public string UserId { get; set; }
       public string Token { get; set; }
    }




}
