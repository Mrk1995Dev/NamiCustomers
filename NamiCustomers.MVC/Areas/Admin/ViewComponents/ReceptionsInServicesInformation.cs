using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;
using NamiCustomers.MVC.Services;

namespace NamiCustomers.MVC.Areas.Admin.ViewComponents
{
    public class ReceptionsInServicesInformation(ISubscriberService subscriberService, ISevenSoftService sevenSoftService) : ViewComponent
    {

        public IViewComponentResult Invoke(string ReceptionCode)
        {
            var result = sevenSoftService.GetReceptionsInServicesInformationByReceptionCode(ReceptionCode, subscriberService.CurrentSubscriber.NationalCode).Result;
            return View(viewName: "ReceptionsInServicesInformation", result.Data);
        }
    }



}
