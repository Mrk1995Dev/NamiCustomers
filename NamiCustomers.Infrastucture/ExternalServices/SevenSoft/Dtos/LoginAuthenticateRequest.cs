namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

public class LoginAuthenticateRequest
{
    public string? Id { get; set; }
    public string UserName { get; set; } = "admin";
    public string Password { get; set; } = "N@mi1405IT##";
    public bool RememberMe { get; set; } = true;
    public string? Returnurl { get; set; }
    public bool IsMobileConfirmed { get; set; } = true;
    public bool IsSystemAccount { get; set; } = true;
    public bool Isf { get; set; } = true;
    public bool TwoFactorEnabled { get; set; } = true;
    public string? MobileNumber { get; set; }
    public int ProductId { get; set; }
    public bool IsCaptchaEnabled { get; set; } = true;
}
