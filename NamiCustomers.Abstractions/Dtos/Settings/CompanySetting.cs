using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamiCustomers.Abstractions.Dtos.Settings;

public class CompanySetting
{
    public string? CompanyName { get; set; }
    public string? Accountnumber { get; set; }
    public string? Shebanumber { get; set; }
    public string? BankName { get; set; }
    public string? BankCode { get; set; }

    public string? BanckBranch { get; set; }
}
