using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NamiCustomers.Abstractions.Dtos.Security.Dto;
using NamiCustomers.Abstractions.Dtos.Security.Dto.Roles;
using NamiCustomers.MVC.Services;
namespace NamiCustomers.MVC.Areas.Admin.Controllers;

//[Authorize(Roles = "Admin")]
[Area("Admin")]
public class UsersController(IUserService userService, IRoleService roleService,IHttpContextAccessor httpContextAccessor) : Controller
{
    public async Task<IActionResult> Index()
    {
        var roles = await userService.GetAllAsync();
        return View(roles.Data);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RegisterDto register)
    {
        if (ModelState.IsValid == false)
        {
            return View(register);
        }

        await userService.RegisterAsync(register);
        return View(register);
    }


   
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var result = await userService.GetAsync(id);

        return View(result.Data);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(string Id)
    {
        var user = (await userService.GetAsync(Id)).Data;
        UserEditDto userEdit = new UserEditDto()
        {
            Email = user.Email,
            FirstName = user.FirstName,
            Id = user.Id,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
            EmailConfirmed = user.EmailConfirmed
        };
        return View(userEdit);

    }
    [HttpPost]
    public async Task<IActionResult> Edit(UserEditDto userEdit)
    {

        var result = await userService.Edit(userEdit);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }
        //string message = "";
        //foreach (var item in result.err.ToList())
        //{
        //    message += item.Description + Environment.NewLine;
        //}
        //TempData["Message"] = message;
        return View(userEdit);
    }

    public async Task<IActionResult> Delete(string Id)
    {
        var user = await userService.GetAsync(Id);
        UserDeleteDto userDelete = new UserDeleteDto()
        {
            Email = user.Data.Email,
            FullName = $"{user.Data.FirstName}  {user.Data.LastName}",
            Id = user.Data.Id,
            UserName = user.Data.UserName,
        };
        var result = await userService.Remove(Id);
        return View(userDelete);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(UserDeleteDto userDelete)
    {
        var result = await userService.Remove(userDelete.Id);


        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }

        //string message = "";
        //foreach (var item in result.Errors.ToList())
        //{
        //    message += item.Description + Environment.NewLine;
        //}
        //TempData["Message"] = message;

        return View(userDelete);
    }

    [HttpGet]
    public async Task<IActionResult> AddUserRole(string Id)
    {
        var user = await userService.GetAsync(Id);
        var allRoles = await roleService.GetAllAsync();

        var roles = new List<SelectListItem>(
            allRoles.Data.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Name,
            }
            ).ToList());

        return View("AddUserRole", new AddUserRoleDto
        {
            Id = Id,
            Roles = roles.Select(c => new KeyValuePair<string, string>(c.Value, c.Text)).ToDictionary(),
            Email = user.Data.Email,
            FullName = $"{user.Data.FirstName}  {user.Data.LastName}"
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddUserRole(AddUserRoleDto newRole)
    {
        //var userId = httpContextAccessor.GetClaimValue(MyClaims.UserId);
        //var user = await userService.GetAsync(userId);
        var user = await userService.GetAsync(newRole.Id); 
        var result = await userService.AddUserRole(newRole);
        return RedirectToAction("UserRoles", "Users", new { user.Data.Id, area = "admin" });
    }

    public async Task<IActionResult> UserRoles(string Id)
    {
        var user = await userService.GetAsync(Id);
        var roles =(await userService.GetRolesAsync(Id)).Data.Roles.Select(c=>c.Value).ToList();
        ViewBag.UserInfo = $"Name : {user.Data.FirstName} {user.Data.LastName} Email:{user.Data.Email}";
        return View(roles);
    }



}
