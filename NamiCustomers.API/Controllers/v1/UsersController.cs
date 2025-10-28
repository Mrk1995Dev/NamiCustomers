using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NamiCustomers.Abstractions.Dtos.Account;
using NamiCustomers.Abstractions.Dtos.Security.Dto;
using NamiCustomers.Abstractions.Dtos.Security.Dto.Roles;
using NamiCustomers.Domain.Entities.Account;

namespace NamiCustomers.API.Controllers.v1;


[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize(Policy = (nameof(MyPloicies.AdminAccess)))]
public class UsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) : ControllerBase
{

    [HttpGet("[action]")]
    public async Task<ResultDto<AddUserRoleDto>> GetUserRolesAsync(string userId)
    {
        try
        {
            if (userId is null)
            {
                return new ResultDto<AddUserRoleDto>(Infrastucture.Properties.Resources.errNotFound, false);
            }
            var user = await userManager.Users.SingleOrDefaultAsync(c => c.Id == userId);
            var roles = await userManager.GetRolesAsync(user);

            var rolesDtos = roleManager.Roles.Where(c => roles.Contains(c.Name)).Select(c => new RoleDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();

            var result = new AddUserRoleDto
            {
                Email = user.Email,
                FullName = user.FullName,
                Id = userId,
                Roles = rolesDtos
            };

            return ResultDto.Success<AddUserRoleDto>(result);
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<AddUserRoleDto>(ex.Message);
        }
    }

    [HttpGet("[action]")]
    public async Task<ResultDto<UserDto>> GetAsync(string id)
    {
        var user = await userManager.Users.Select(p => new UserDto
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            UserName = p.UserName,
            PhoneNumber = p.PhoneNumber,
            EmailConfirmed = p.EmailConfirmed,
            AccessFailedCount = p.AccessFailedCount,
            Email = p.Email
        }).FirstOrDefaultAsync(c => c.Id == id);

        return ResultDto.Success<UserDto>(user);
    }

    [HttpGet("[action]")]
    public async Task<ResultDto<List<UserDto>>> GetAllAsync()
    {
        var users = await userManager.Users
            .Select(p => new UserDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                UserName = p.UserName,
                PhoneNumber = p.PhoneNumber,
                EmailConfirmed = p.EmailConfirmed,
                AccessFailedCount = p.AccessFailedCount,
                Email = p.Email
            }).ToListAsync();
        return ResultDto.Success<List<UserDto>>(users);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register(RegisterUserDto register)
    {
        if (ModelState.IsValid == false)
        {
            return BadRequest(ResultDto.Failure(Infrastucture.Properties.Resources.errSave));
        }

        ApplicationUser newUser = new ApplicationUser()
        {
            FirstName = register.FirstName,
            LastName = register.LastName,
            Email = register.Email,
            UserName = register.Email,
            PassWord = register.Password,
            EmailConfirmed = true,
            PhoneNumber = register.Mobile,
            NationalCode = register.NationalCode,
            Id = Guid.NewGuid().ToString()

        };

        var result = await userManager.CreateAsync(newUser, register.Password);
        if (result.Succeeded)
        {
            return Ok(ResultDto.Success<ApplicationUser>(newUser));
        }

        return BadRequest(ResultDto.Failure<ApplicationUser>(string.Join(",", result.Errors.Select(c => c.Description).ToList())));

    }



    [HttpPut("[action]")]
    public async Task<IActionResult> Edit(UserEditDto userEdit)
    {
        var user = await userManager.FindByIdAsync(userEdit.Id);
 
        user.FirstName = userEdit.FirstName;
        user.LastName = userEdit.LastName;
        user.PhoneNumber = userEdit.PhoneNumber;
        user.Email = userEdit.Email;
        user.UserName = userEdit.UserName;
        user.EmailConfirmed = userEdit.EmailConfirmed;
        var result = await userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return Ok(ResultDto.Success(Infrastucture.Properties.Resources.msgEdited));
        }

        return BadRequest(ResultDto.Failure(string.Join(",", result.Errors.Select(c => c.Description).ToList())));
    }


    [HttpDelete("[action]")]
    public async Task<IActionResult> Remove(string id)
    {
        var user = userManager.FindByIdAsync(id).Result;
        if (user is null)
        {
            return BadRequest(ResultDto.Success(Infrastucture.Properties.Resources.msgDeleted));
        }
        var result = userManager.DeleteAsync(user).Result;

        if (result.Succeeded)
        {
            return Ok(ResultDto.Success(Infrastucture.Properties.Resources.msgDeleted));
        }

        return BadRequest(ResultDto.Failure(string.Join(",", result.Errors.Select(c => c.Description).ToList())));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> AddUserRole(AddUserRoleDto newRole)
    {
        try
        {
            var user = await userManager.FindByIdAsync(newRole.Id);
            var role = roleManager.Roles.FirstOrDefault(c => c.Id == newRole.Role);
            var result = await userManager.AddToRoleAsync(user, role.Name);
            return Ok(ResultDto.Success(Infrastucture.Properties.Resources.msgSave));

        }
        catch (Exception ex)
        {
            return BadRequest(ResultDto.Failure(ex.Message));
        }

    }

    [HttpPost("[action]")]
    public async Task<IActionResult> RemoveUserRole(AddUserRoleDto newRole)
    {
        try
        {
            var user = await userManager.FindByIdAsync(newRole.Id);
            var result = await userManager.RemoveFromRoleAsync(user, newRole.Role);
            return Ok(ResultDto.Success(Infrastucture.Properties.Resources.msgDeleted));
        }
        catch (Exception ex)
        {
            return BadRequest(ResultDto.Failure(ex.Message));
        }
    }
}
