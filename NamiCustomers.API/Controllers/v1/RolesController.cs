using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NamiCustomers.Abstractions.Dtos.Security.Dto;
using NamiCustomers.Abstractions.Dtos.Security.Dto.Roles;
using NamiCustomers.Domain.Entities.Account;
using System.Data;

namespace NamiCustomers.API.Controllers.v1;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class RolesController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<ResultDto<List<RoleListDto>>>  GetAllAsync()
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

    [HttpGet("[action]")]
    public async Task<ResultDto<RoleListDto>> GetAsync(string id)
    {
        var role = await roleManager.Roles.Where(c=>c.Id==id)
            .Select(p =>
             new RoleListDto
             {
                 Id = p.Id,
                 Description = p.Description,
                 Name = p.Name
             })
            .FirstOrDefaultAsync();
        return new ResultDto<RoleListDto>(Infrastucture.Properties.Resources.msgFound, true, role);
    }

 

    [HttpPost("[action]")]
    public async Task<ResultDto> Register(AddNewRoleDto newRole)
    {
        ApplicationRole role = new ApplicationRole()
        {
            Description = newRole.Description,
            Name = newRole.Name,
            Id=Guid.NewGuid().ToString()
        };
        var result = roleManager.CreateAsync(role).Result;

        //_roleManager.UpdateAsync()
        //_roleManager.DeleteAsync()
        if (result.Succeeded)
        {
            return new ResultDto(Infrastucture.Properties.Resources.msgFound, true);
        }
        ;
        //ViewBag.Errors = result.Errors.ToList();
        return new ResultDto(Infrastucture.Properties.Resources.errSave, false);

    }


    [HttpPut("[action]")]
    public async Task<ResultDto> Edit(RoleListDto roleEdit)
    {
        var role = await roleManager.FindByIdAsync(roleEdit.Id);
        
        role.Description=roleEdit.Description;
        role.Name=roleEdit.Name;

        var result = await roleManager.UpdateAsync(role);

        if (result.Succeeded)
        {
            return new ResultDto(Infrastucture.Properties.Resources.msgEdited, true);
        }
        string message = "";
        foreach (var item in result.Errors.ToList())
        {
            message += item.Description + Environment.NewLine;
        }
        //TempData["Message"] = message;
        return new ResultDto(Infrastucture.Properties.Resources.errEdited, false);
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
