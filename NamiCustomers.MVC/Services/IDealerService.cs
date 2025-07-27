
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using System.Net.Http;

namespace NamiCustomers.MVC.Services;

public interface IDealerService
{
    /// <summary>
    /// لیست نمایندگی ها
    /// </summary>
    /// <returns></returns>
    Task<ResultDto<DealerResponse[]>> GetDealersAsync();
    /// <summary>
    /// لیست شعب
    /// </summary>
    /// <param name="dealerId"></param>
    /// <returns></returns>
    Task<ResultDto<GetBranchesByDealerResponse[]>> GetBranchesByDealerAsync(Guid dealerId);
    /// <summary>
    /// سوابق تعمیراتی
    /// </summary>
    /// <param name="chassisVinNumber"></param>
    /// <returns></returns>
    Task<ResultDto<GetReceptionsInformationByVinNumberResponse[]>> GetReceptionsInformationByVinNumber(string chassisVinNumber);
}
public class DealerService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : IDealerService
{
    public async Task<ResultDto<DealerResponse[]>> GetDealersAsync()
    {
        var response = await httpClient.GetFromJsonAsync<ResultDto<DealerResponse[]>>($"Dealer/GetDealers");
        if (response.Data != null)
        {
            return new ResultDto<DealerResponse[]>(Infrastucture.Properties.Resources.msgFound
           , true, response.Data);
        }
        return new ResultDto<DealerResponse[]>(Infrastucture.Properties.Resources.errNotFound, false);
    }

    public async Task<ResultDto<GetBranchesByDealerResponse[]>> GetBranchesByDealerAsync(Guid dealerId)
    {
        var response = await httpClient.GetFromJsonAsync<ResultDto<GetBranchesByDealerResponse[]>>($"Dealer/GetBranchesByDealer?DealerId={dealerId}");
        if (response.Data != null)
        {
            return new ResultDto<GetBranchesByDealerResponse[]>(Infrastucture.Properties.Resources.msgFound
           , true, response.Data);
        }
        return new ResultDto<GetBranchesByDealerResponse[]>(Infrastucture.Properties.Resources.errNotFound, false);
    }
    public async Task<ResultDto<GetReceptionsInformationByVinNumberResponse[]>> GetReceptionsInformationByVinNumber(string chassisVinNumber)
    {
        var response = await httpClient.GetFromJsonAsync<ResultDto<GetReceptionsInformationByVinNumberResponse[]>>($"Dealer/GetReceptionsInformationByVinNumber?chassisVinNumber={chassisVinNumber}");
        if (response.Data != null)
        {
            return new ResultDto<GetReceptionsInformationByVinNumberResponse[]>(Infrastucture.Properties.Resources.msgFound
           , true, response.Data);
        }
        return new ResultDto<GetReceptionsInformationByVinNumberResponse[]>(Infrastucture.Properties.Resources.errNotFound, false);
    }
}