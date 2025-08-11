using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Infrastucture.ExternalServices.Nami;
using NamiCustomers.MVC.Services;


using NamiCustomers.MVC.Services.Auth;
using NuGet.Common;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NamiCustomers.MVC.Controllers;


public class HomeController(ITokenSessionService tokenSessionService,INamiKhodroService namiKhodroService) : Controller
{
    public IActionResult Index()
    {
        if (tokenSessionService.IsExpired)
        {
            return RedirectToAction("LogOut", "Account");
        }
        return View();
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Contact()
    { return View(); }

    public IActionResult About()
    {
        return View();
    }
    public async Task<IActionResult> NamiNews()
    {
        var data=await  namiKhodroService.GetNamiNews();
        return View(data);
       // return View();
    }

    public IActionResult faq()
	{
		return View();
	}
}
