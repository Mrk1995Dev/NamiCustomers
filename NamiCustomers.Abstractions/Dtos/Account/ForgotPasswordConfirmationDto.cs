using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Abstractions.Dtos.Account
{
    public class ForgotPasswordConfirmationDto
    {
        [Required]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }
}
