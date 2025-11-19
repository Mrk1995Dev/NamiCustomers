using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

namespace NamiCustomers.MVC.Services;

public interface IDealerService
{


    /// <summary>
    /// فراخوان
    /// </summary>
    /// <returns></returns>
    Task<ResultDto<ReceptionsInformationByVinNumberResponse[]>> GetReceptionsInformationByVinNumberAsync();
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
    Task<ResultDto<BranchesByDealerResponse[]>> GetBranchesByDealerAsync(Guid dealerId);
    /// <summary>
    /// سوابق تعمیراتی
    /// </summary>
    /// <param name="chassisVinNumber"></param>
    /// <returns></returns>
    Task<ResultDto<ReceptionsInformationByVinNumberResponse[]>> GetReceptionsInformationByVinNumber();
}
public class DealerService(HttpClient httpClient, ISubscriberService subscriberService) : IDealerService
{

    public async Task<ResultDto<ReceptionsInformationByVinNumberResponse[]>> GetReceptionsInformationByVinNumberAsync()
    {
        var chassisVinNumber = subscriberService.CurrentSubscriber.VehicleModels.FirstOrDefault(c => c.IsDefault)?.VinNumber;
        if (string.IsNullOrEmpty(chassisVinNumber))
        {
            return   ResultDto.Failure<ReceptionsInformationByVinNumberResponse[]>(Infrastucture.Properties.Resources.errNotFound);
        }

        var response = await httpClient.GetFromJsonAsync<ResultDto<ReceptionsInformationByVinNumberResponse[]>>($"Dealer/GetReceptionsInformationByVinNumber?chassisVinNumber={chassisVinNumber}");
        if (response.Data != null)
        {
            return   ResultDto.Success<ReceptionsInformationByVinNumberResponse[]>( response.Data);
        }
        return   ResultDto.Failure<ReceptionsInformationByVinNumberResponse[]>(Infrastucture.Properties.Resources.errNotFound);
    }
    public async Task<ResultDto<DealerResponse[]>> GetDealersAsync()
    {
        var response = await httpClient.GetFromJsonAsync<ResultDto<DealerResponse[]>>($"Dealer/GetDealers");
        if (response.Data != null)
        {
            return   ResultDto.Success<DealerResponse[]>( response.Data);
        }
        return   ResultDto.Failure<DealerResponse[]>(Infrastucture.Properties.Resources.errNotFound);
    }

    public async Task<ResultDto<BranchesByDealerResponse[]>> GetBranchesByDealerAsync(Guid dealerId)
    {
        var response = await httpClient.GetFromJsonAsync<ResultDto<BranchesByDealerResponse[]>>($"Dealer/GetBranchesByDealer?DealerId={dealerId}");
        if (response.Data != null)
        {
            return   ResultDto.Success<BranchesByDealerResponse[]>( response.Data);
        }
        return   ResultDto.Failure<BranchesByDealerResponse[]>(Infrastucture.Properties.Resources.errNotFound);
    }
    public async Task<ResultDto<ReceptionsInformationByVinNumberResponse[]>> GetReceptionsInformationByVinNumber()
    {
        var chassisVinNumber = subscriberService.CurrentSubscriber.VehicleModels.FirstOrDefault(c => c.IsDefault)?.VinNumber;
        if (string.IsNullOrEmpty(chassisVinNumber))
        {
            return   ResultDto.Failure<ReceptionsInformationByVinNumberResponse[]>(Infrastucture.Properties.Resources.errNotFound );
        }
        var response = await httpClient.GetFromJsonAsync<ResultDto<ReceptionsInformationByVinNumberResponse[]>>($"Dealer/GetReceptionsInformationByVinNumber?chassisVinNumber={chassisVinNumber}");
        if (response.Data != null)
        {
            return   ResultDto.Success<ReceptionsInformationByVinNumberResponse[]>( response.Data);
        }
        return   ResultDto.Failure<ReceptionsInformationByVinNumberResponse[]>(Infrastucture.Properties.Resources.errNotFound);
    }
}