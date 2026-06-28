using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Web.Services.Account.Dto;

public class ResetPasswordDto
{
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
    public string UserId { get; set; }
    public string Token { get; set; }
    public List<string> Errors { get; set; } = new();
}
