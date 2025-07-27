using Microsoft.AspNetCore.Mvc;
using NamiCustomers.MVC.Services;

namespace NamiCustomers.MVC.Controllers;

public class DealerController(IDealerService dealerService) : MyBaseController
{
    public async Task<IActionResult> Dealers()
    {
        var result = await   dealerService.GetDealersAsync();

        if (result.Succeeded)
            return View(result.Data);

        return MyError(result.Errors);
 
    }
    public async Task<IActionResult> DealerInfo(DealerResponse dealerResponse)
    {
        return View(dealerResponse);
    }

    public async Task<IActionResult> GetBranches(Guid dealerId)
    {
        var result = await dealerService.GetBranchesByDealerAsync(dealerId);
        if (result.Succeeded)
            return View(result.Data);

        return MyError(result.Errors);
    }
   
    public async Task<IActionResult> GetReceptionsInformationByVinNumber(string vinNumber)
    {
        var result = await dealerService.GetReceptionsInformationByVinNumber(vinNumber);
        if (result.Succeeded)
            return View(result.Data);

        return MyError(result.Errors);
        
    }
}
