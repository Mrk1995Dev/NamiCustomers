using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.MVC.Services;

namespace NamiCustomers.MVC.Areas.Admin.ViewComponents
{
    public class DefaultVin(ISubscriberService subscriberService, IVehicleService vehicleService) : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (subscriberService.CurrentSubscriber == null)
            {
                return View(viewName: "DefaultVin",new VehicleModelDto());
            }
            var data = vehicleService.GetAllAsync(subscriberService.CurrentSubscriber.Id).Result;

            return View(viewName: "DefaultVin", data?.Data?.FirstOrDefault(c => c.IsDefault));
        }
    }
}
