using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.Win32;
using NamiCustomers.Abstractions.Dtos.Security.Dto.Roles;
using NamiCustomers.MVC.Controllers;
using NamiCustomers.MVC.Services;

namespace NamiCustomers.MVC.Areas.Admin.Controllers;

[Authorize(Policy = nameof(MyPloicies.AdminAccess))]
[Area("Admin")]
public class RolesController(IRoleService roleService) : MyBaseController
{
    public async Task<IActionResult> Index()
    {
        var roles = await roleService.GetAllAsync();
        return View(roles.Data);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddNewRoleDto newRole)
    {
        
        //if (!ModelState.IsValid)
        //{
        //    SetModelStateError();
        //    return View(newRole);
        //}

        var result = await roleService.RegisterAsync(newRole);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Roles", new { area = "Admin" });
        }
        ;
        ViewBag.Errors = result.Message;
        return View(newRole);

    }
    [HttpGet]
    public async Task<IActionResult> UserInRole(string Name)
    {
        var result = await roleService.GetUsersInRole(Name);

        return View(result.Data);
    }
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var result = await roleService.GetAsync(id);

        return View(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string Id)
    {
        var role = (await roleService.GetAsync(Id)).Data;
        return View(role);

    }
    [HttpPost]
    public async Task<IActionResult> Edit(RoleDto role)
    {

        var result = await roleService.Edit(role);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Roles", new { area = "Admin" });
        }
        //string message = "";
        //foreach (var item in result.err.ToList())
        //{
        //    message += item.Description + Environment.NewLine;
        //}
        //TempData["Message"] = message;
        return View(role);
    }


}
