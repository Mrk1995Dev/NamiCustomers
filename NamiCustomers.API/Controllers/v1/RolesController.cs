using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NamiCustomers.Abstractions.Dtos.Security.Dto;
using NamiCustomers.Abstractions.Dtos.Security.Dto.Roles;
using NamiCustomers.Domain.Entities.Account;
using System.Data;

namespace NamiCustomers.API.Controllers.v1;

[Authorize(Roles = "Admin")]
[Area("Admin")]
public class RolesController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager) : Controller
{
    [HttpGet("[action]")]
    public async Task<ResultDto<List<RoleListDto>>>   GetAllAsync()
    {
        var roles =await  roleManager.Roles
            .Select(p =>
             new RoleListDto
             {
                 Id = p.Id,
                 Description = p.Description,
                 Name = p.Name
             })
            .ToListAsync();
        return new ResultDto<List<RoleListDto>>(Infrastucture.Properties.Resources.msgFound, true, roles);
    }

    [HttpPost("[action]")]
    public async Task<ResultDto> Register(AddNewRoleDto newRole)
    {
        ApplicationRole role = new ApplicationRole()
        {
            Description = newRole.Description,
            Name = newRole.Name,
        };
        var result = roleManager.CreateAsync(role).Result;

        //_roleManager.UpdateAsync()
        //_roleManager.DeleteAsync()
        if (result.Succeeded)
        {
            return new ResultDto(Infrastucture.Properties.Resources.msgFound, true);
        }
        ;
        ViewBag.Errors = result.Errors.ToList();
        return new ResultDto(Infrastucture.Properties.Resources.errSave, false);

    }
    [HttpGet("[action]")]
    public async Task<ResultDto<List<UserListDto>>> GetUsersInRole(string Name)
    {
        var usersInRole =await userManager.GetUsersInRoleAsync(Name);

        var users= usersInRole.Select(p => new UserListDto
        {
            FirstName = p.FirstName,
            LastName = p.LastName,
            UserName = p.UserName,
            PhoneNumber = p.PhoneNumber,
            Id = p.Id,
        }).ToList();

        return new ResultDto<List<UserListDto>>(Infrastucture.Properties.Resources.msgFound, true, users);
    }
}
