using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamiCustomers.Domain.Entities.Security
{
    public class ApplicationUser : IdentityUser
    {
        // می‌توانید ویژگی‌های اضافی کاربر را اینجا اضافه کنید
        public string FullName { get; set; }
    }

}
