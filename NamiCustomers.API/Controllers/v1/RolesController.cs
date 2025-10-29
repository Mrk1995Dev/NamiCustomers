using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using NamiCustomers.Abstractions.Dtos.Security.Dto;
using NamiCustomers.Abstractions.Dtos.Security.Dto.Roles;
using NamiCustomers.Domain.Entities.Account;
using System.Data;

namespace NamiCustomers.API.Controllers.v1;


[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize(Policy = (nameof(MyPloicies.AdminAccess)))]
public class RolesController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllAsync()
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
        return Ok(ResultDto.Success<List<RoleDto>>(roles));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAsync(string id)
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
        return Ok( ResultDto.Success<RoleDto>(role));
    }



    [HttpPost("[action]")]
    public async Task<IActionResult> Register(AddNewRoleDto newRole)
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
            return Ok( ResultDto.Success<IdentityResult>(result));
        }
        ;

        return BadRequest( ResultDto.Failure<IdentityResult>(string.Join(",", result.Errors.Select(c => c.Description).ToList())));

    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Edit(RoleDto roleEdit)
    {
        var role = await roleManager.FindByIdAsync(roleEdit.Id);

        role.Description = roleEdit.Description;
        role.Name = roleEdit.Name;

        var result = await roleManager.UpdateAsync(role);

        if (result.Succeeded)
        {
            return Ok( ResultDto.Success<IdentityResult>(result));
        }
        return BadRequest(ResultDto.Failure<IdentityResult>(string.Join(",", result.Errors.Select(c => c.Description).ToList())));
    }
   
    [HttpGet("[action]")]
    public async Task<IActionResult> GetUsersInRole(string Name)
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

        return Ok(ResultDto.Success(users));
    }
}
