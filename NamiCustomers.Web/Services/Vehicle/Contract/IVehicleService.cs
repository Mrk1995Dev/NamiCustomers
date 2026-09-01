using NamiCustomers.Abstractions.Dtos;
using NamiCustomers.Web.Services.Vehicle.Dto;
using System.ComponentModel;

namespace NamiCustomers.Web.Services.Vehicle.Contract
{
    public interface IVehicleService
    {
        Task<ResultDto<List<VehicleModelDto>>> GetSubscriberVehicleAsync();
        Task<ResultDto<VehicleModelDto>> GetSubscriberVehicleInfoAsync(int? id);
        Task<ResultDto<ActiveMainChassisGuaranteeResponse>> GetActiveMainChassisGuarantee(string vinNumber);
        Task<ResultDto<PartsPriceByChassisResponse[]>> GetPartsPriceByChassisAsync(PartsPriceByChassisRequest getPartsPriceByChassisRequest);
        Task<ResultDto<ServicesPriceResponse[]>> GetServicesPriceList(ServicesPriceRequest request);
        Task<ResultDto<string[]>> GetSpecificCasesAsync(string vinNumber);
        Task<ResultDto<SpOrderingsBySubscriberDto[]>> GetSpOrderingsBySubscriberAsync(string chassisVinNumber, string nationalCodeOrEconomicCode);
    }
}