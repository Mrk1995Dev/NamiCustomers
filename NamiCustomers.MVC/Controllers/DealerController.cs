using Microsoft.AspNetCore.Mvc;
using NamiCustomers.MVC.Services;

namespace NamiCustomers.MVC.Controllers;

public class DealerController(IDealerService dealerService) : MyBaseController
{
    public async Task<IActionResult> Dealers()
    {
        SetError(new List<string> { "vf..phojoi" });

        var result = await dealerService.GetDealersAsync();
        return View(result.Data);
        if (!result.Succeeded)
        {
            SetError(result.Errors);
        }
        return View(result.Data);
    }
    public async Task<IActionResult> DealerInfo(DealerResponse dealerResponse)
    {
        return View(dealerResponse);
    }

    public async Task<IActionResult> GetBranches(Guid dealerId)
    {
        var result = await dealerService.GetBranchesByDealerAsync(dealerId);
        if (!result.Succeeded)
        {
            SetError(result.Errors);
        }
        return View(result.Data);
    }

    public async Task<IActionResult> ReceptionsInformationByVinNumber()
    {
        var result = await dealerService.GetReceptionsInformationByVinNumber();
        if (!result.Succeeded)
        {
            SetError(result.Errors);
        }
        return View(result.Data);
    }
}
