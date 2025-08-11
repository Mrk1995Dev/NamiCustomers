using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Abstractions.Dtos.Account
{
    public class GetOtpDto
    {
        [Required]
        [Display(Name = "تلفن همراه")]
        public string Mobile { get; set; }

    }
}
