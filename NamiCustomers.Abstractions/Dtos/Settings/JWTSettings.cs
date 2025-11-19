namespace NamiCustomers.Abstractions.Dtos.Settings;

public class JWTSettings
{
    public string securityKey { get; set; }
    public string validIssuer { get; set; }
    public string validAudience { get; set; }
    public int expiryInMinutes { get; set; }
    public string LogInProvider { get; set; }
}

