using Microsoft.AspNetCore.Identity;

namespace NamiCustomers.Domain.Entities.Account;

public class ApplicationUser : IdentityUser
{
    // می‌توانید ویژگی‌های اضافی کاربر را اینجا اضافه کنید
    public string FullName => $"{FirstName} {LastName}";
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PassWord { get; set; }
    public string? NationalCode { get; set; }
}
