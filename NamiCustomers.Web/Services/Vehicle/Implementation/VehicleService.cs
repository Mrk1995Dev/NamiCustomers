using NamiCustomers.Abstractions.Dtos;
using NamiCustomers.Web.Services.Vehicle.Contract;
using NamiCustomers.Web.Services.Vehicle.Dto;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;

namespace NamiCustomers.Web.Services.Vehicle.Implementation
{
    public class VehicleService(HttpClient httpClient) : IVehicleService
    {
        public async Task<ResultDto<List<VehicleModelDto>>> GetSubscriberVehicleAsync()
        {
            var result = new ResultDto<List<VehicleModelDto>>("", false);
            var response = await httpClient.GetAsync("Vehicle/GetAll");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new ResultDto<List<VehicleModelDto>>("", false)
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
            }

            else if(response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<List<VehicleModelDto>>>();
                return result;
            }

            else
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<List<VehicleModelDto>>>();
                return result;
            }
        }

        public async Task<ResultDto<ActiveMainChassisGuaranteeResponse>> GetActiveMainChassisGuarantee(string vinNumber)
        {
            var result = new ResultDto<ActiveMainChassisGuaranteeResponse>("", false);
            var response = await httpClient.GetAsync($"Vehicle/GetActiveMainChassisGuarantee?vinNumber={vinNumber}");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new ResultDto<ActiveMainChassisGuaranteeResponse>("", false)
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
            }

            else if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<ActiveMainChassisGuaranteeResponse>>();
                return result;
            }

            else
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<ActiveMainChassisGuaranteeResponse>>();
                return result;
            }
        }

        public async Task<ResultDto<VehicleModelDto>> GetSubscriberVehicleInfoAsync(int? id)
        {
            var result = new ResultDto<VehicleModelDto>("", false);
            var response = await httpClient.GetAsync($"Vehicle/Get?id={id}");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new ResultDto<VehicleModelDto>("", false)
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
            }

            else if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<VehicleModelDto>>();
                return result;
            }

            else
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<VehicleModelDto>>();
                return result;
            }
        }

        public async Task<ResultDto<PartsPriceByChassisResponse[]>> GetPartsPriceByChassisAsync(PartsPriceByChassisRequest getPartsPriceByChassisRequest)
        {
            var result = new ResultDto<PartsPriceByChassisResponse[]>("", false);
            var response = await httpClient
                .GetAsync($"Vehicle/GetPartsPriceByChassis?" +
                $"VehicleModelId={getPartsPriceByChassisRequest.VehicleModelId}" +
                $"&PartNo={getPartsPriceByChassisRequest.PartNo}" +
                $"&ChassisVinNumber={getPartsPriceByChassisRequest.ChassisVinNumber}" +
                $"&NationalCodeOrEconomicCode={getPartsPriceByChassisRequest.NationalCodeOrEconomicCode}" +
                $"&PartName={getPartsPriceByChassisRequest.PartName}" +
                $"&PartSupplierNo={getPartsPriceByChassisRequest.PartSupplierNo}");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new ResultDto<PartsPriceByChassisResponse[]>("", false)
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
            }

            else if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<PartsPriceByChassisResponse[]>>();
                return result;
            }

            else
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<PartsPriceByChassisResponse[]>>();
                return result;
            }
        }

        public async Task<ResultDto<ServicesPriceResponse[]>> GetServicesPriceList(ServicesPriceRequest request)
        {
            var result = new ResultDto<ServicesPriceResponse[]>("", false);
            var response = await httpClient.PostAsJsonAsync("Vehicle/ServicesPriceList", request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new ResultDto<ServicesPriceResponse[]>("", false)
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
            }

            else if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<ServicesPriceResponse[]>>();
                return result;
            }

            else
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<ServicesPriceResponse[]>>();
                return result;
            }
        }
    }
}