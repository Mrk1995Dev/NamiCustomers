using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NamiCustomers.MVC.Services.Auth;
using NuGet.Common;
using System.Diagnostics;

namespace NamiCustomers.MVC.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITokenSessionService _tokenSessionService;

    public HomeController(ILogger<HomeController> logger, ITokenSessionService  tokenSessionService)
    {
        _logger = logger;
        _tokenSessionService = tokenSessionService;
    }
		 
		public IActionResult Index()
    {
        if (_tokenSessionService.IsExpired)
        {
            return RedirectToAction("LogOut","Account");
        }
        return View();
    }

 
    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
