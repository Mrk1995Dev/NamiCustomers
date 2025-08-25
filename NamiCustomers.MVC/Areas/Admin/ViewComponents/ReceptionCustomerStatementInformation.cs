using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;
using NamiCustomers.MVC.Services;

namespace NamiCustomers.MVC.Areas.Admin.ViewComponents
{
    public class ReceptionCustomerStatementInformation(ISubscriberService subscriberService, ISevenSoftService sevenSoftService) : ViewComponent
    {
        public IViewComponentResult Invoke(string ReceptionCode)
        {
            var result = sevenSoftService.GetReceptionCustomerStatementInformationByReceptionCode(ReceptionCode, subscriberService.CurrentSubscriber.NationalCode).Result;
            return View(viewName: "ReceptionCustomerStatementInformation", result.Data);
        }
    }



}
