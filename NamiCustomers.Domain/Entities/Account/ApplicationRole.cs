using Microsoft.AspNetCore.Identity;

namespace NamiCustomers.Domain.Entities.Account;

public class ApplicationRole : IdentityRole<string>
{
    public string Description { get; set; }
}
