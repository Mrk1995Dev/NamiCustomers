using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.MVC.Models.Account
{
	public class GetOtpDto
	{
		[Required]
		[Display(Name = "تلفن همراه")]
		public string Mobile { get; set; }
		
	}
}
