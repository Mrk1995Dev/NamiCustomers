using Microsoft.AspNetCore.Identity;
using NamiCustomers.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NamiCustomers.Domain.Entities;

public class Menu
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required string Icon { get; set; }
    public required string Route { get; set; }
    public int Order { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid? ParentMenuId { get; set; }
    public Menu? ParentMenu { get; set; }
    public ICollection<Menu> SubMenus { get; set; } = [];
    public ICollection<ApplicationRole> Roles { get; set; } = [];
}
 

