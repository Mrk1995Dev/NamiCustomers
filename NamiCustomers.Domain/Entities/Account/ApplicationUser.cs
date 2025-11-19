using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace NamiCustomers.Domain.Entities.Account;

public class ApplicationUser : IdentityUser<string>
{

    // می‌توانید ویژگی‌های اضافی کاربر را اینجا اضافه کنید
    public string FullName => $"{FirstName} {LastName}";
    /// <summary>
    /// نام کوچک
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// فامیلی
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// پسورد هش شده
    /// </summary>
    public string PassWord { get; set; }
    public string? NationalCode { get; set; }
    /// <summary>
    /// موبایل
    /// </summary>
    public string? Mobile { get; set; }

    public List<ApplicationUserToken> ApplicationUserTokens { get; set; }
}

/// <summary>
/// توکن‌های کاربر
/// </summary>
public class ApplicationUserToken : IdentityUserToken<string>
{
    public TokenType TokenType { get; set; } = TokenType.AccessToken;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExp { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}

/// <summary>
/// نوع توکن
/// </summary>
public enum TokenType
{
    [Description("AccessToken")]
    AccessToken = 1,
    [Description("RefreshToken")]
    RefreshToken = 2,
    [Description("Both")]
    Both = 3,
    [Description("RefreshTokenExp")]
    RefreshTokenExp = 4
}
