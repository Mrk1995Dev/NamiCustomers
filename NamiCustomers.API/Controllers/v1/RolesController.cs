using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NamiCustomers.Abstractions.Dtos.Security.Dto;
using NamiCustomers.Abstractions.Dtos.Security.Dto.Roles;
using NamiCustomers.Domain.Entities.Account;
using System.Data;

namespace NamiCustomers.API.Controllers.v1;


[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class RolesController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<ResultDto<List<RoleDto>>> GetAllAsync()
    {
        var roles = await roleManager.Roles
            .Select(p =>
             new RoleDto
             {
                 Id = p.Id,
                 Description = p.Description,
                 Name = p.Name
             })
            .ToListAsync();
        return new ResultDto<List<RoleDto>>(Infrastucture.Properties.Resources.msgFound, true, roles);
    }

    [HttpGet("[action]")]
    public async Task<ResultDto<RoleDto>> GetAsync(string id)
    {
        var role = await roleManager.Roles.Where(c => c.Id == id)
            .Select(p =>
             new RoleDto
             {
                 Id = p.Id,
                 Description = p.Description,
                 Name = p.Name
             })
            .FirstOrDefaultAsync();
        return new ResultDto<RoleDto>(Infrastucture.Properties.Resources.msgFound, true, role);
    }



    [HttpPost("[action]")]
    public async Task<ResultDto> Register(AddNewRoleDto newRole)
    {
        ApplicationRole role = new ApplicationRole()
        {
            Description = newRole.Description,
            Name = newRole.Name,
            Id = Guid.NewGuid().ToString()
        };
        var result = roleManager.CreateAsync(role).Result;

        //_roleManager.UpdateAsync()
        //_roleManager.DeleteAsync()
        if (result.Succeeded)
        {
            return  ResultDto.Success(Infrastucture.Properties.Resources.msgFound);
        }
        ;
        //ViewBag.Errors = result.Errors.ToList();
        return   ResultDto.Failure(Infrastucture.Properties.Resources.errSave);

    }


    [HttpPut("[action]")]
    public async Task<ResultDto> Edit(RoleDto roleEdit)
    {
        var role = await roleManager.FindByIdAsync(roleEdit.Id);

        role.Description = roleEdit.Description;
        role.Name = roleEdit.Name;

        var result = await roleManager.UpdateAsync(role);

        if (result.Succeeded)
        {
            return ResultDto.Success(Infrastucture.Properties.Resources.msgEdited);
        }
        string message = "";
        foreach (var item in result.Errors.ToList())
        {
            message += item.Description + Environment.NewLine;
        }
        //TempData["Message"] = message;
        return   ResultDto.Failure(Infrastucture.Properties.Resources.errEdited);
    }





    [HttpGet("[action]")]
    public async Task<ResultDto<List<UserDto>>> GetUsersInRole(string Name)
    {
        var usersInRole = await userManager.GetUsersInRoleAsync(Name);

        var users = usersInRole.Select(p => new UserDto
        {
            FirstName = p.FirstName,
            LastName = p.LastName,
            UserName = p.UserName,
            PhoneNumber = p.PhoneNumber,
            Id = p.Id,
        }).ToList();

        return new ResultDto<List<UserDto>>(Infrastucture.Properties.Resources.msgFound, true, users);
    }


}
