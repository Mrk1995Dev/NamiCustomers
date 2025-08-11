using Microsoft.AspNetCore.Mvc;
using NamiCustomers.MVC.Services;

namespace NamiCustomers.MVC.Controllers;

public class DealerController(IDealerService dealerService) : MyBaseController
{


    public async Task<IActionResult> Call()
    {
        var result = await dealerService.GetReceptionsInformationByVinNumberAsync();
        if (!result.Succeeded)
        {
            SetError(result.Errors.FirstOrDefault());
        }
        return View(result.Data);
    }

    public async Task<IActionResult> Dealers()
    {
        var result = await dealerService.GetDealersAsync();
        if (!result.Succeeded)
        {
            SetError(result.Errors.FirstOrDefault());
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
            SetError(result.Errors.FirstOrDefault());
        }
        return View(result.Data);
    }

    public async Task<IActionResult> ReceptionsInformationByVinNumber()
    {
        var result = await dealerService.GetReceptionsInformationByVinNumber();
        if (!result.Succeeded)
        {
            SetError(result.Errors.FirstOrDefault());
        }
        return View(result);
    }
}
