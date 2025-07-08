using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NamiCustomers.Abstractions.Dtos.Security.Dto.Roles
{
    public class RoleDto
    {
        [Display(Name = "شناسه")]
        public string Id { get; set; }
        [Display(Name = "عنوان لاتین")]
        public string Name { get; set; }
        [Display(Name = "عنوان فارسی")]
        public string Description { get; set; }
    }
}
