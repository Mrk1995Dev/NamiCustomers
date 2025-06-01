using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NamiCustomers.MVC.Areas.Admin.Models.Dto.Roles
{
    public class AddUserRoleDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }
        public string Id { get; set; }
        [Display(Name ="نقش")]
        public string Role { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }
}
