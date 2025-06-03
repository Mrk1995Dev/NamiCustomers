using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NamiCustomers.Infrastucture.Model.Account;

public class LoginDto
{
    [Required]
    [EmailAddress]
    [Display(Name = "ایمیل")]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "رمز")]
    public string Password { get; set; }

    [Display(Name ="مرا به خاطر بسپار")]
    public bool IsPersistent { get; set; } = false;

    public string ReturnUrl { get; set; }

    public string OtpCode { get; set; }
    public string Mobile { get; set; }
}
