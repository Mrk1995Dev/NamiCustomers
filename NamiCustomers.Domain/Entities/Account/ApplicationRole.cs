using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Domain.Entities.Account;

public class ApplicationRole : IdentityRole<string>
{
 

    [Display(Name = "عنوان فارسی")]
    public string Description { get; set; }
}
