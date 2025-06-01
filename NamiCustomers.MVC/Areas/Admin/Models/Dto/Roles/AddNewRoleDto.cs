using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NamiCustomers.MVC.Areas.Admin.Models.Dto.Roles
{
    public class AddNewRoleDto
    {
        [Display(Name ="عنوان لاتین")]
        public string Name { get; set; }
        [Display(Name = "عنوان فارسی")]
        public string Description { get; set; }
    }
}
