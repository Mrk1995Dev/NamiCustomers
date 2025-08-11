using Microsoft.AspNetCore.Mvc;
using NamiCustomers.MVC.Services;

namespace NamiCustomers.MVC.Areas.Admin.ViewComponents
{
    public class VinMenu(ISubscriberService subscriberService, IVehicleService vehicleService) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var data = vehicleService.GetAllAsync(subscriberService.CurrentSubscriber.Id).Result;

            return View(viewName: "VinMenu", data.Data);
        }
    }
}
