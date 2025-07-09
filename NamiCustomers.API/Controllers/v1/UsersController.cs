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
public class UsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> roleManager;

    public UsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    [HttpGet("[action]")]
    public async Task<ResultDto<AddUserRoleDto>> GetUserRolesAsync(string userId)
    {
        if (userId is null)
        {
            return new ResultDto<AddUserRoleDto>(Infrastucture.Properties.Resources.errNotFound, false,new AddUserRoleDto());
        }
        var user = await userManager.Users.SingleOrDefaultAsync(c => c.Id == userId);
        var roles = await userManager.GetRolesAsync(user);

        var rolesDtos = roleManager.Roles.Where(c => roles.Contains(c.Name)).Select(c => new KeyValuePair<string, string>(c.Id, c.Name
            )).ToDictionary();

        var result = new AddUserRoleDto
        {
            Email = user.Email,
            FullName = user.FullName,
            Id = userId,
            Roles = rolesDtos
        };


        return new ResultDto<AddUserRoleDto>(Infrastucture.Properties.Resources.msgFound, true, result);
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

        return new ResultDto<UserDto>(Infrastucture.Properties.Resources.msgFound, true, user);
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
        return new ResultDto<List<UserDto>>(Infrastucture.Properties.Resources.msgFound, true, users);
    }

    [HttpPost("[action]")]
    public async Task<ResultDto> Register(RegisterUserDto register)
    {
        if (ModelState.IsValid == false)
        {
            return new ResultDto(Infrastucture.Properties.Resources.errSave, false);
        }

        ApplicationUser newUser = new ApplicationUser()
        {
            FirstName = register.FirstName,
            LastName = register.LastName,
            Email = register.Email,
            UserName = register.Email,
            PassWord = register.Password
            ,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(newUser, register.Password);
        if (result.Succeeded)
        {
            return new ResultDto(Infrastucture.Properties.Resources.msgSave, true);
        }

        string message = "";
        foreach (var item in result.Errors.ToList())
        {
            message += item.Description + Environment.NewLine;
        }
        //TempData["Message"] = message;
        return new ResultDto(Infrastucture.Properties.Resources.errSave, false);
    }



    [HttpPut("[action]")]
    public async Task<ResultDto> Edit(UserEditDto userEdit)
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


    [HttpDelete("[action]")]
    public async Task<ResultDto> Remove(string id)
    {
        var user = userManager.FindByIdAsync(id).Result;

        var result = userManager.DeleteAsync(user).Result;

        if (result.Succeeded)
        {
            return new ResultDto(Infrastucture.Properties.Resources.msgDeleted, true);
        }

        string message = "";
        foreach (var item in result.Errors.ToList())
        {
            message += item.Description + Environment.NewLine;
        }
        //TempData["Message"] = message;

        return new ResultDto(Infrastucture.Properties.Resources.errDelete, false);
    }
    [HttpPost("[action]")]
    public async Task<ResultDto> AddUserRole(AddUserRoleDto newRole)
    {
        var user = await userManager.FindByIdAsync(newRole.Id);
        var result = await userManager.AddToRoleAsync(user, newRole.Role);
        return new ResultDto(Infrastucture.Properties.Resources.msgSave, true);
    }

    [HttpPost("[action]")]
    public async Task<ResultDto> RemoveUserRole(AddUserRoleDto newRole)
    {
        var user = await userManager.FindByIdAsync(newRole.Id);
        var result = await userManager.RemoveFromRoleAsync(user, newRole.Role);
        return new ResultDto(Infrastucture.Properties.Resources.msgSave, true);
    }
}
