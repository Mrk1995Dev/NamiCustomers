using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NamiCustomers.MVC.Areas.Admin.Models.Dto
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
