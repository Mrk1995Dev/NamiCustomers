using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.MVC.Services;

namespace NamiCustomers.MVC.Areas.Admin.ViewComponents
{
    public class VinMenu(ISubscriberService subscriberService, IVehicleService vehicleService) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (subscriberService.CurrentSubscriber == null)
            {
                return View(viewName: "VinMenu");
            }
            var data = vehicleService.GetAllAsync(subscriberService.CurrentSubscriber.Id).Result;

            return View(viewName: "VinMenu", data.Data);
        }
    }
}
