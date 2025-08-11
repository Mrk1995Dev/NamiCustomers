using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Abstractions.Dtos.Security.Dto
{
    public class UserDeleteDto
    {
        [Display(Name = "شناسه")]
        public string Id { get; set; }
        [Display(Name = " نام ونام خانوادگی")]
        public string FullName { get; set; }
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }
}
