using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.JsonWebTokens;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using NamiCustomers.Infrastucture.Utilities;
using NamiCustomers.MVC.Services;
using NamiCustomers.MVC.Services.Auth;
using Newtonsoft.Json;

namespace NamiCustomers.MVC.Controllers;

//[Authorize(Policy = nameof(MyPloicies.SubscriberAccess))]
public class VehicleController(
    ISubscriberService subscriberService,
    IVehicleService vehicleService,
    ISevenSoftService sevenSoftService,
    IAuthService authService,
    IHttpContextAccessor httpContextAccessor
    ) : MyBaseController
{
    //[ServiceFilter(typeof(VinFilter))]
    [HttpGet]
    public async Task<IActionResult> ActiveMainChassisGuarantee()
    {
        var vinNumber = subscriberService.CurrentSubscriber.VehicleModels?.FirstOrDefault(c => c.IsDefault)?.VinNumber;
        if (vinNumber != null)
        {
            var data = await vehicleService.GetActiveMainChassisGuarantee(vinNumber);
            if (!data.Succeeded)
            {
                return View(new ActiveMainChassisGuaranteeResponse() { VinNumber = vinNumber });
            }
            data.Data.VinNumber = vinNumber;
            return View(data.Data);
        }
        return View(new ActiveMainChassisGuaranteeResponse());
    }
    [HttpGet]
    public async Task<IActionResult> ServicesPrice(ServicesPriceRequest? request)
    {
        if (request.DealerId == Guid.Empty)
        {
            var dealers = await sevenSoftService.GetDealers();
            List<SelectListItem> list = new List<SelectListItem> { new SelectListItem { Text = "", Value = Guid.Empty.ToString(), Selected = true } };
            var items = dealers.Select(c => new SelectListItem { Text = $"{c.DealerName}-{c.DealerNo}", Value = c.UniqueId }).ToList();
            list.AddRange(items);
            ViewBag.Dealers = list;
            request = new ServicesPriceRequest();
        }
        return View(request);
    }






    [HttpGet]
    public JsonResult GetBranchesByDealer(Guid dealerId)
    {
        var data = sevenSoftService.GetBranchesByDealer(dealerId).Result;
        var branches = data.Where(c => c.BranchNo == "200").Select(c => new SelectListItem { Text = $" {c.BranchName}-کد {c.BranchNo} -گرید {c.BranchGrade}", Value = c.UniqueId }).ToList();

        return Json(branches);
    }






    [HttpPost]
    public async Task<IActionResult> ServicesPriceList(ServicesPriceRequest request)
    {
        if (string.IsNullOrEmpty(request.ServiceCode) && string.IsNullOrEmpty(request.ServiceName))
        {
            request.DealerId = Guid.Empty;
            request.BranchId = Guid.Empty;
            SetError(Infrastucture.Properties.Resources.errRequiredServiceCodeName);
            return RedirectToAction("ServicesPrice", request);
        }
        var subscriber = subscriberService.CurrentSubscriber;
        var vinNumber = subscriber.VehicleModels?.FirstOrDefault(c => c.IsDefault)?.VinNumber;
        if (vinNumber != null)
        {
            request.NationalCodeOrEconomicCode = subscriber.NationalCode;
            request.ChassisVinNumber = vinNumber;
            var data = await sevenSoftService.GetServicesPriceByBranchId(request);
            if (!data.Succeeded)
            {
                SetError(data.Message);
                return RedirectToAction("ServicesPrice", request);
            }
            return View(data);
        }
        return RedirectToAction("ServicesPrice", request);
    }



    // [ServiceFilter(typeof(VinFilter))]
    public async Task<IActionResult> Details(int id)
    {
        var result = await vehicleService.GetAsync(id);

        if (!result.Succeeded)
        {
            SetError(result.Errors.FirstOrDefault());
        }
        return View(result.Data);
    }
    public async Task<IActionResult> Default(int id)
    {
        var result = await vehicleService.SetDefaultAsync(id);
        if (!result.Succeeded)
        {
            SetError(result.Errors.FirstOrDefault());
        }

        return RedirectToAction("Index", "Home");
    }


    public async Task<IActionResult> Index()
    {
        var result = await vehicleService.GetAllAsync(subscriberService.CurrentSubscriber.Id);
        if (!result.Succeeded)
        {
            SetError(result.Errors.FirstOrDefault());
        }
        return View(result.Data);
    }
    [HttpGet]
    public async Task<IActionResult> Create(VehicleModelDto? vehicleModelDto)
    {
        if (vehicleModelDto is null)
        {
            vehicleModelDto = new VehicleModelDto { SubscriberId = subscriberService.CurrentSubscriber.Id };
        }

        return View(vehicleModelDto);
    }

    [HttpGet]
    public async Task<IActionResult> ChassisInformationByVinNumber()
    {
        return View(new VehicleModelDto { SubscriberId = subscriberService.CurrentSubscriber.Id, VinNumber = subscriberService.CurrentSubscriber.VehicleModels?.FirstOrDefault(c => c.IsDefault)?.VinNumber });
    }

    [HttpGet]
    public async Task<IActionResult> GetChassisInformationByVinNumber(string vinNumber)
    {
        if (string.IsNullOrEmpty(vinNumber))
        {
            SetError("شماره شاسی را وارد نمایید");
            return RedirectToAction("ChassisInformationByVinNumber");
        }
        var data = await vehicleService.GetChassisInformationByVinNumber(vinNumber);
        if (data != null)
        {
            return RedirectToAction("Create", data.Data);
        }
        return View(new VehicleModelDto { SubscriberId = subscriberService.CurrentSubscriber.Id });
    }

    [HttpPost]
    public async Task<IActionResult> CreateVehicle(VehicleModelDto vehicleModelDto)
    {
        if (!ModelState.IsValid)
        {
            SetError(Infrastucture.Properties.Resources.errInputInValid);
            return RedirectToAction("Index");
        }
        var result = await vehicleService.RegisterAsync(vehicleModelDto);

        if (!result.Succeeded)
        {
            SetError(result.Errors.FirstOrDefault());
        }
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await vehicleService.GetAsync(id);
        if (!result.Succeeded)
        {
            SetError(result.Errors.FirstOrDefault());
        }
        return View(result.Data);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(VehicleModelDto vehicleModelDto)
    {
        if (!ModelState.IsValid)
        {
            SetError(Infrastucture.Properties.Resources.errInputInValid);
            return RedirectToAction("Index");
        }

        var result = await vehicleService.EditAsync(vehicleModelDto);
        if (!result.Succeeded)
        {
            SetError(result.Message);
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await vehicleService.RemoveAsync(id);
        if (!result.Succeeded)
        {
            SetError(result.Message);
        }
        return RedirectToAction("Index");
    }
    /// <summary>
    /// لیست سفارشات
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> OrderingsBySubscriber()
    {
        var subscriber = subscriberService.CurrentSubscriber;
        var vinNumber = subscriber.VehicleModels?.FirstOrDefault(c => c.IsDefault)?.VinNumber;
        //toDo
        var result = await sevenSoftService.GetSpOrderingsBySubscriber("LGBL4AE07RD064874", "2360736541");//subscriber.NationalCode
        return View(result);
    }
    /// <summary>
    /// ریز سفارشات
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> SpOrderingPartSpOrderingCode(string id)
    {
        var result = await sevenSoftService.GetSpOrderingPartSpOrderingCode(id);
        return View(result);
    }
    //------------------
    [HttpGet]
    public async Task<IActionResult> PartsPriceByChassis(PartsPriceByChassisRequest? getPartsPriceByChassisRequest)
    {
        if (getPartsPriceByChassisRequest == null)
        {
            getPartsPriceByChassisRequest = new PartsPriceByChassisRequest();
        }

        return View(getPartsPriceByChassisRequest);
    }

    /// <summary>
    /// استعلام بهای قطعات
    /// </summary>
    /// <param name="getPartsPriceByChassisRequest"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PartsPriceDetailsByChassis(PartsPriceByChassisRequest getPartsPriceByChassisRequest)
    {
        if (string.IsNullOrEmpty(getPartsPriceByChassisRequest.PartName) && string.IsNullOrEmpty(getPartsPriceByChassisRequest.PartNo))
        {
            SetError(Infrastucture.Properties.Resources.errInputInValid);
            return RedirectToAction("PartsPriceByChassis");
        }
        var subscriber = subscriberService.CurrentSubscriber;
        var vinNumber = subscriber.VehicleModels?.FirstOrDefault(c => c.IsDefault)?.VinNumber;

        getPartsPriceByChassisRequest.ChassisVinNumber = vinNumber;
        getPartsPriceByChassisRequest.NationalCodeOrEconomicCode = subscriber.NationalCode;

        var result = await sevenSoftService.GetPartsPriceByChassis(getPartsPriceByChassisRequest);

        if (!result.Succeeded)
        {
            SetError(result.Message);
            return RedirectToAction("PartsPriceByChassis", getPartsPriceByChassisRequest);
        }
        return View("PartsPriceDetailsByChassis", result.Data);
    }

    //[HttpGet("[action]")]
    //public async Task<IActionResult> GetAllOrderStatusType()
    //{
    //    var orderStatusList = await sevenSoftService.GetAllOrderStatusType();
    //    List<SelectListItem> list = new List<SelectListItem> { new SelectListItem { Text = "", Value = Guid.Empty.ToString(), Selected = true } };
    //    var items = orderStatusList.Select(c => new SelectListItem { Text = $"{c.OrderStatusTypeLocalizedName}-{c.Code}", Value = c.UniqueId }).ToList();
    //    list.AddRange(items);
    //    ViewBag.OrderStatusList = list;

    //}







}
