namespace NamiCustomers.Infrastucture.Model.Account
{
    public class LoginRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Otp { get; set; } 
        public string Mobile { get; set; }
    }
}
