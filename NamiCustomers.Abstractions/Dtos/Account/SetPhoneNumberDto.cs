using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Abstractions.Dtos.Account
{
    public class SetPhoneNumberDto
    {
        [Required]
        [RegularExpression(@"(\+98|0)?9\d{9}")]
        [Display(Name = "شماره موبایل")]
        public string PhoneNumber { get; set; }
    }
}
