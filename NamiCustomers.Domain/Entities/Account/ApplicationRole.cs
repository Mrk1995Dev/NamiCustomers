using Microsoft.AspNetCore.Identity;

namespace NamiCustomers.Domain.Entities.Account;

public class ApplicationRole : IdentityRole<int>
{
    public ICollection<Menu> Menus { get; set; } = [];
}
