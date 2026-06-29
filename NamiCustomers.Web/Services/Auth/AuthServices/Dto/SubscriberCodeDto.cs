using System.ComponentModel.DataAnnotations;
namespace NamiCustomers.Web.Services.Auth.AuthServices.Dto;

public class SubscriberCodeDto
{
    public string AuthCode { get; set; }


    [Display(Name = "موبایل")]
    public string Mobile { get; set; }

    [Display(Name = "کد ملی")]
    public string NationalCode { get; set; }
}