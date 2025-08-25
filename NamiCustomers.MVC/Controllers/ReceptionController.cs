using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using NamiCustomers.MVC.Services;

namespace NamiCustomers.MVC.Controllers;

public class ReceptionController(ISevenSoftService sevenSoftService,ISubscriberService subscriberService,IDealerService dealerService) : MyBaseController
{
    public async Task<IActionResult> InformationOfReception(string ReceptionCode)
    {
        return View("~/Views/Reception/InformationOfReception.cshtml", ReceptionCode);
    }

    /////// <summary>
    /////// دریافت اطلاعات قطعات پذیرش براساس کد پذیرش و کدملی
    /////// </summary>
    /////// <param name="ReceptionCode"></param>
    /////// <param name="NationalCodeOrEconomicCode"></param>
    /////// <returns></returns>
    ////public async Task<IActionResult> ReceptionsPartsInformation(string ReceptionCode)
    ////{

    ////    var result = await sevenSoftService.GetReceptionsPartsInformationByReceptionCode(ReceptionCode, subscriberService.CurrentSubscriber.NationalCode);
    ////    if (!result.Succeeded)
    ////    {
    ////        return SetError(result.Errors.FirstOrDefault());
    ////    }
    ////    return PartialView(result.Data);
    ////}
    /// <summary>
    /// دریافت اطلاعات خدمات داخل تعمیرگاه پذیرش براساس کد پذیرش و کدملی
    /// </summary>
    /// <param name="ReceptionCode"></param>
    /// <param name="NationalCodeOrEconomicCode"></param>
    /// <returns></returns>
    //public async Task<IActionResult> ReceptionsInServicesInformation(string ReceptionCode= "701-200-250" )
    //{
    //    var result = await sevenSoftService.GetReceptionsInServicesInformationByReceptionCode(ReceptionCode, subscriberService.CurrentSubscriber.NationalCode);
    //    if (!result.Succeeded)
    //    {
    //        return SetError(result.Errors.FirstOrDefault());
    //    }
    //    return View(result.Data);
    //}
    /// <summary>
    /// دریافت اطلاعات خدمات خارج از تعمیرگاه پذیرش ها براساس کد پذیرش و کدملی
    /// </summary>
    /// <param name="ReceptionCode"></param>
    /// <param name="NationalCodeOrEconomicCode"></param>
    /// <returns></returns>
    public async Task<IActionResult> ReceptionsOutServicesInformation(string ReceptionCode = "701-200-250")
    {
        var result = await sevenSoftService.GetReceptionsOutServicesInformationByReceptionCode(ReceptionCode, subscriberService.CurrentSubscriber.NationalCode);
        if (!result.Succeeded)
        {
            return SetError(result.Errors.FirstOrDefault());
        }
        return View(result.Data);
    }
    /// <summary>
    /// دریافت اطلاعات اظهارات مشتری براساس کد پذیرش و کدملی
    /// </summary>
    /// <param name="ReceptionCode"></param>
    /// <param name="NationalCodeOrEconomicCode"></param>
    /// <returns></returns>
    //public async Task<IActionResult> ReceptionCustomerStatementInformation(string ReceptionCode = "701-200-250" )
    //{
    //    var result = await sevenSoftService.GetReceptionCustomerStatementInformationByReceptionCode(ReceptionCode ,subscriberService.CurrentSubscriber.NationalCode);
    //    if (!result.Succeeded)
    //    {
    //        return SetError(result.Errors.FirstOrDefault());
    //    }
    //    return View(result.Data);
    //}
    /// <summary>
    /// فاکتور پذیرش
    /// </summary>
    /// <param name="ReceptionCode"></param>
    /// <returns></returns>
    public async Task<IActionResult> ReceptionsInformation(string ReceptionCode = "701-200-250")
    {
        var result = await sevenSoftService.GetReceptionsInformationByReceptionID(ReceptionCode);
        if (!result.Succeeded)
        {
            return SetError(result.Errors.FirstOrDefault());
        }
        return View(result.Data);
    }
    /// <summary>
    /// سوابق تعمیراتی
    /// </summary>
    /// <returns></returns>
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
