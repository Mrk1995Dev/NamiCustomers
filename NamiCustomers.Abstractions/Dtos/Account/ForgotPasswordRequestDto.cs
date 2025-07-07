namespace NamiCustomers.Abstractions.Dtos.Account;

public class ForgotPasswordRequestDto
{
    public string Email { get; set; }
    public string CallBAckUrl { get; set; }
}
