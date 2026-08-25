namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

public class LoginAuthenticateResponse
{
    public string? Token { get; set; }
    public string? AccessToken { get; set; }
    public string? access_token { get; set; }
    public string? Jwt { get; set; }
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
}
