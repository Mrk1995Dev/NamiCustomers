using Microsoft.CodeAnalysis;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Infrastucture.Utilities;
using System.Text.Json;

namespace NamiCustomers.MVC.Services;

public interface IVehicleService
{
    Task<ResultDto<VehicleModelDto>> RegisterAsync(VehicleModelDto vehicleModelDto);
    Task<ResultDto> RemoveAsync(int id);
    Task<ResultDto<VehicleModelDto>> GetAsync(int id);
    Task<ResultDto<VehicleModelDto>> SetDefaultAsync(int id);
    Task<ResultDto<List<VehicleModelDto>>> GetAllAsync(int subscriberId);
    Task<ResultDto> EditAsync(VehicleModelDto updateCustomer);
    Task<ResultDto<VehicleModelDto>> GetChassisInformationByVinNumber(string vinNumber);
    Task<ResultDto<ActiveMainChassisGuaranteeResponse>> GetActiveMainChassisGuarantee(string vinNumber);
    Task<ResultDto<List<string>>> GetSpecificCases();
}


public class VehicleService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, ISubscriberService subscriberService) : IVehicleService
{
    private async Task GetToken()
    {
        var mobile = httpContextAccessor.GetClaimValue(MyClaims.Mobile);
        var myToken = await httpClient.GetFromJsonAsync<MyToken>($"Account/GetToken?mobile={mobile}");

        httpClient.DefaultRequestHeaders.Add("accept", "*/*");
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {myToken.token}");
    }

    public async Task<ResultDto> EditAsync(VehicleModelDto vehicleModelDto)
    {
        var response = await httpClient.PutAsJsonAsync($"Vehicle/edit", vehicleModelDto);
        if (response.IsSuccessStatusCode)
        {
            return   ResultDto.Success (Infrastucture.Properties.Resources.msgEdited);
        }

        return  ResultDto.Failure(Infrastucture.Properties.Resources.errEdited);
    }

    public async Task<ResultDto> RemoveAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"Vehicle/remove?id={id}");
        if (response.IsSuccessStatusCode)
        {
            return   ResultDto.Success(Infrastucture.Properties.Resources.msgDeleted);
        }

        return  ResultDto.Failure(Infrastucture.Properties.Resources.errDelete);
    }

    public async Task<ResultDto<VehicleModelDto>> GetAsync(int id)
    {
        var response = await httpClient.GetFromJsonAsync<ResultDto<VehicleModelDto>>($"Vehicle/Get?id={id}");
        if (response.Data != null)
        {
            return   ResultDto.Success<VehicleModelDto>( response.Data);
        }
        return   ResultDto.Failure<VehicleModelDto>(Infrastucture.Properties.Resources.errNotFound);
    }
    public async Task<ResultDto<VehicleModelDto>> SetDefaultAsync(int id)
    {
        var response = await httpClient.PutAsJsonAsync($"Vehicle/SetDefault/{id}", new { id = id });
        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<ResultDto<VehicleModelDto>>();
            return   ResultDto.Success<VehicleModelDto>( dto.Data);
        }
        return   ResultDto.Failure<VehicleModelDto>(Infrastucture.Properties.Resources.errEdited);
    }


    public async Task<ResultDto<VehicleModelDto>> GetChassisInformationByVinNumber(string vinNumber)
    {
        var response = await httpClient.GetFromJsonAsync<ResultDto<VehicleModelDto>>($"Vehicle/GetChassisInformationByVinNumber?vinNumber={vinNumber}");
        if (response.Data != null)
        {
            return   ResultDto.Success<VehicleModelDto>(response.Data);
        }
        return   ResultDto.Failure<VehicleModelDto>(Infrastucture.Properties.Resources.errNotFound);
    }

    public async Task<ResultDto<List<string>>> GetSpecificCases()
    {
        string vinNumber = subscriberService.CurrentSubscriber.VehicleModels.FirstOrDefault(c => c.IsDefault)?.VinNumber;
        string nationalCodeOrEconomicCode = subscriberService.CurrentSubscriber.NationalCode;
        string mobile=subscriberService.CurrentSubscriber.Mobile;
        var response = await httpClient.GetFromJsonAsync<ResultDto<string[]>>($"Vehicle/GetSpecificCases?vinNumber={vinNumber}&nationalCodeOrEconomicCode={nationalCodeOrEconomicCode}&mobile={mobile}");
        if (response != null)
        {
            if (!response.Data.Any())
            {
                //diablo حالت خاص 
                return   ResultDto.Success<List<string>>( new List<string> { Infrastucture.Properties.Resources.msgNotFoundAnyResult });
            }
            return   ResultDto.Success < List<string> >( response.Data.ToList());
        }
        return   ResultDto.Failure<List<string>>(Infrastucture.Properties.Resources.errNotFound);
    }




    public async Task<ResultDto<List<VehicleModelDto>>> GetAllAsync(int subscriberId)
    {
        var response = await httpClient.GetFromJsonAsync<ResultDto<List<VehicleModelDto>>>($"Vehicle/GetAll?subscriberId={subscriberId}");
        if (response.Succeeded)
        {
            return response;
        }
        return   ResultDto.Failure<List<VehicleModelDto>>(Infrastucture.Properties.Resources.errNotFound);
    }




    public async Task<ResultDto<VehicleModelDto>> RegisterAsync(VehicleModelDto vehicleRegisterDto)
    {
        var nationalCode = httpContextAccessor.GetClaimValue(MyClaims.NationalCode);
        await GetToken();

        vehicleRegisterDto.SubscriberId = subscriberService.CurrentSubscriber.Id;

        var response = await httpClient.PostAsJsonAsync($"Vehicle/Register", vehicleRegisterDto);

        if (response.IsSuccessStatusCode)
        {

            var result = JsonSerializer.Deserialize<ResultDto<VehicleModelDto>>(response.Content.ReadAsStringAsync().Result);
            if (result.Succeeded)
            {
                return   ResultDto.Success<VehicleModelDto>( result.Data);
            }
            else
            {
                return ResultDto.Failure<VehicleModelDto>(result.Message);
          
            }

        }
        return   ResultDto.Failure<VehicleModelDto>(
             Infrastucture.Properties.Resources.errSave
              );

    }

    public async Task<ResultDto<ActiveMainChassisGuaranteeResponse>> GetActiveMainChassisGuarantee(string vinNumber)
    {
        var response = await httpClient.GetFromJsonAsync<ResultDto<ActiveMainChassisGuaranteeResponse>>($"Vehicle/GetActiveMainChassisGuarantee?vinNumber={vinNumber}");
        if (response.Succeeded)
        {
            return response;
        }
        return ResultDto.Failure<ActiveMainChassisGuaranteeResponse>(Infrastucture.Properties.Resources.errNotFound);
    }
}