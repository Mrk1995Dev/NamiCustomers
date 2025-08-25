using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;
using NamiCustomers.MVC.Services;

namespace NamiCustomers.MVC.Areas.Admin.ViewComponents
{
    public class ReceptionsOutServicesInformation(ISubscriberService subscriberService, ISevenSoftService sevenSoftService) : ViewComponent
    {
        public IViewComponentResult Invoke(string ReceptionCode)
        {
            var result = sevenSoftService.GetReceptionsOutServicesInformationByReceptionCode(ReceptionCode, subscriberService.CurrentSubscriber.NationalCode).Result;

            return View(viewName: "ReceptionsOutServicesInformation", result.Data);
        }
    }



}
