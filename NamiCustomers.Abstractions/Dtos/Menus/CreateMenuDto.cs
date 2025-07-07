using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamiCustomers.Abstractions.Dtos.Menus
{

    public class CreateMenuDto
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Icon { get; set; }
        public required string Route { get; set; }
        public int Order { get; set; }
        public Guid? ParentMenuId { get; set; }
        public List<string> AllowedRoles { get; set; } = [];
    }
}
