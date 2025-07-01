using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Infrastucture.Model.Account
{
	public class GetOtpDto
	{
		[Required]
		[Display(Name = "تلفن همراه")]
		public string Mobile { get; set; }
		
	}
}
