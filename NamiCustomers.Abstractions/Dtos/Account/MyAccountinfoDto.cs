using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NamiCustomers.Infrastucture.Model.Account
{
    public class MyAccountinfoDto
    {
        [Display(Name = "شناسه")]
        public string Id { get; set; }
        [Display(Name = "نام ونام خانوادگی")]
        public string FullName { get; set; }
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "تلفن")]
        public string PhoneNumber { get; set; }
        [Display(Name = "ایمیل")]
        public bool EmailConfirmed { get; set; }
        [Display(Name = "شماره تلفن تایید شده")]
        public bool PhoneNumberConfirmed { get; set; }
        [Display(Name = "ورود مرحله ای")]
        public bool TwoFactorEnabled { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "رمز")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool IsPersistent { get; set; } = false;
    }
}
