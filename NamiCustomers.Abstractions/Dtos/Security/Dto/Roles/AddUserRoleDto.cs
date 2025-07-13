using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Abstractions.Dtos.Security.Dto.Roles
{
    public class AddUserRoleDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }
        public string Id { get; set; }
        [Display(Name ="نقش")]
        public string Role { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
 

}
