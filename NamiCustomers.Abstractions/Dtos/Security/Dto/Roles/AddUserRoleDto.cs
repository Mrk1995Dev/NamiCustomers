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
        public Dictionary<string,string> Roles { get; set; }
    }
 

}
