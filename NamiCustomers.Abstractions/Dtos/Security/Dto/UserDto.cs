using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Abstractions.Dtos.Security.Dto;

public class UserDto
{
    [Display(Name = "شناسه")]
    public string Id { get; set; }
    [Display(Name = "نام")]
    public string FirstName { get; set; }
    [Display(Name = "نام خانوادگی")]
    public string LastName { get; set; }
    [Display(Name = "نام کاربری")]
    public string UserName { get; set; }
    [Display(Name = "تلفن")]
    public string PhoneNumber { get; set; }
    [Display(Name = "ایمیل")]
    public bool EmailConfirmed { get; set; }
    [Display(Name = "خطا در ورود")]
    public int AccessFailedCount { get; set; }
    [Display(Name = "ایمیل")]
    public string Email { get; set; }

}
