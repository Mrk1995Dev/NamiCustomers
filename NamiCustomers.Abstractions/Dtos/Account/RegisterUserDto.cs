using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NamiCustomers.Abstractions.Dtos.Account;

public class RegisterUserDto
{
    [Required]
    [Display(Name ="نام")]
    public string FirstName { get; set; }
    [Display(Name = "نام خانوادگی")]
    [Required]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    [Display(Name = "ایمیل")]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "گذر واژه")]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare( nameof(Password))]
    [Display(Name = "تکرارگذر واژه")]
    public string ConfirmPassword { get; set; }
    public string CallbakUrl { get; set; }
}

public class RegisterModelDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Mobile { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string NationalCode { get; set; }
}
