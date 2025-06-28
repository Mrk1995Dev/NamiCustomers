using NamiCustomers.Application.Services.SevenSoftServices;
using System.Reflection;

namespace NamiCustomers.API.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]

public class SAMPLEController(ISevenSoftService sevenSoftService) : ControllerBase
{
    [HttpGet("[action]")]
    public virtual async Task<IActionResult> GetSubscriberByNationalCode(string nationalCode= "0082425639")
    {
        var a =await  sevenSoftService.GetSubscriberByNationalCode(nationalCode);
        return Ok(a);
    }
    [HttpGet("[action]")]
    public virtual async Task<IActionResult> GetChassisInformationByVinNumber(string vinNumber = "LGBH9VEA4RY757904")
    {
        var a = await sevenSoftService.GetChassisInformationByVinNumber(vinNumber);
        return Ok(a);
    }

    [HttpGet("[action]")]
    public virtual async Task<IActionResult> GetRelationCustomerInfoByVinNumber(string chassisVinNumber= "LGBH9VEA4RY757904",string 
        nationalCodeOrEconomicCode = "2450185811",string  mobile = "09115656557")
    {
        var a = await sevenSoftService.GetRelationCustomerInfoByVinNumber(chassisVinNumber,nationalCodeOrEconomicCode,mobile);
        return Ok(a);
    }
    //
}
