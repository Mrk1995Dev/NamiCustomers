namespace NamiCustomers.Web.Services.Auth.AuthServices.Dto;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }
    public string Mobile { get; set; }
    public string Id { get; set; }
}
