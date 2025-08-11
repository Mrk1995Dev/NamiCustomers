using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Abstractions.Dtos.Settings;

public class CompanySetting
{
    [Display(Name = "نام شرکت")]
    public string? CompanyName { get; set; }
    public string? Accountnumber { get; set; }
    public string? Shebanumber { get; set; }
    public string? BankName { get; set; }
    public string? BankCode { get; set; }

    public string? BanckBranch { get; set; }
}
