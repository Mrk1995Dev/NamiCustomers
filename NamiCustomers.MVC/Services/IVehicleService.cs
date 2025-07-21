using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Infrastucture.Utilities;
using System.Numerics;
using System.Text.Json;

namespace NamiCustomers.MVC.Services;

public interface IVehicleService
{
    Task<ResultDto<VehicleModelDto>> RegisterAsync(VehicleModelDto vehicleModelDto);
    Task<ResultDto> RemoveAsync(int id);
    Task<ResultDto<VehicleModelDto>> GetAsync(int id);
    Task<ResultDto<List<VehicleModelDto>>> GetAllAsync(int subscriberId);
    Task<ResultDto> EditAsync(VehicleModelDto updateCustomer);
    Task<ResultDto<VehicleModelDto>> GetChassisInformationByVinNumber(string vinNumber);
    Task<ResultDto<ActiveMainChassisGuaranteeResponse>> GetActiveMainChassisGuarantee(string vinNumber);
}


public class VehicleService : IVehicleService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ISubscriberService subscriberService;

    public VehicleService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, ISubscriberService subscriberService)
    {
        _httpClient = httpClient;
        this.httpContextAccessor = httpContextAccessor;
        this.subscriberService = subscriberService;
    }

    private async Task GetToken()
    {
        var mobile = httpContextAccessor.GetClaimValue(MyClaims.Mobile);
        var myToken = await _httpClient.GetFromJsonAsync<MyToken>($"Account/GetToken?mobile={mobile}");

        _httpClient.DefaultRequestHeaders.Add("accept", "*/*");
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {myToken.token}");
    }

    public async Task<ResultDto> EditAsync(VehicleModelDto vehicleModelDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"Vehicle/edit", vehicleModelDto);
        if (response.IsSuccessStatusCode)
        {
            return new ResultDto(Infrastucture.Properties.Resources.msgEdited, true);
        }

        return new ResultDto(Infrastucture.Properties.Resources.errEdited, false);
    }

    public async Task<ResultDto> RemoveAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"Vehicle/remove?id={id}");
        if (response.IsSuccessStatusCode)
        {
            return new ResultDto(Infrastucture.Properties.Resources.msgDeleted, true);
        }

        return new ResultDto(Infrastucture.Properties.Resources.errDelete, false);
    }

    public async Task<ResultDto<VehicleModelDto>> GetAsync(int id)
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<VehicleModelDto>>($"Vehicle/Get?id={id}");
        if (response.Data != null)
        {
            return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.msgFound
           , true, response.Data);
        }
        return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.errNotFound, false);
    }

    public async Task<ResultDto<VehicleModelDto>> GetChassisInformationByVinNumber(string vinNumber)
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<VehicleModelDto>>($"Vehicle/GetChassisInformationByVinNumber?vinNumber={vinNumber}");
        if (response.Data != null)
        {
            return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.msgFound, true,
            response.Data);
        }
        return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.errNotFound, false);
    }




    public async Task<ResultDto<List<VehicleModelDto>>> GetAllAsync(int subscriberId)
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<List<VehicleModelDto>>>($"Vehicle/GetAll?subscriberId={subscriberId}");
        if (response.Succeeded)
        {
            return response;
        }
        return new ResultDto<List<VehicleModelDto>>(Infrastucture.Properties.Resources.errNotFound, false);
    }




    public async Task<ResultDto<VehicleModelDto>> RegisterAsync(VehicleModelDto vehicleRegisterDto)
    {
        var nationalCode = httpContextAccessor.GetClaimValue(MyClaims.NationalCode);
        await GetToken();

        vehicleRegisterDto.SubscriberId = subscriberService.CurrentSubscriber.Id;

        var response = await _httpClient.PostAsJsonAsync($"Vehicle/Register", vehicleRegisterDto);

        if (response.IsSuccessStatusCode)
        {

            var result = JsonSerializer.Deserialize<ResultDto<VehicleModelDto>>(response.Content.ReadAsStringAsync().Result);
            if (result.Succeeded)
            {
                return new ResultDto<VehicleModelDto>(
              Infrastucture.Properties.Resources.msgSave
               , true,
               result.Data);
            }
            else
            {
                return new ResultDto<VehicleModelDto>(
           result.Message,
           false,
           null
           ,
           errors:new List<string> { result.Message}
           );
            }

        }
        return new ResultDto<VehicleModelDto>(
             Infrastucture.Properties.Resources.errSave, false
              );

    }

    public async Task<ResultDto<ActiveMainChassisGuaranteeResponse>> GetActiveMainChassisGuarantee(string vinNumber)
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<ActiveMainChassisGuaranteeResponse>>($"Vehicle/GetActiveMainChassisGuarantee?vinNumber={vinNumber}");
        if (response.Succeeded)
        {
            return response;
        }
        return new ResultDto<ActiveMainChassisGuaranteeResponse>(Infrastucture.Properties.Resources.errNotFound, false);
    }
}