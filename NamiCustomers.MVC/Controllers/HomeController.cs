using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NamiCustomers.MVC.Services;


using NamiCustomers.MVC.Services.Auth;
using NuGet.Common;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NamiCustomers.MVC.Controllers;


public class HomeController(ILogger<HomeController> logger, ITokenSessionService tokenSessionService, IVehicleService vehicleService) : Controller
{
    public IActionResult Index()
    {
        if (tokenSessionService.IsExpired)
        {
            return RedirectToAction("LogOut", "Account");
        }
        return View();
    }
    public async Task<IActionResult> Dealers()
    {
        var data =await   vehicleService.GetDealersAsync();
        return View(data.Data);
    }
    public async Task<IActionResult> DealerInfo(DealerResponse dealerResponse)
    {
        return View(dealerResponse);
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
}
