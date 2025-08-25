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
            return new ResultDto<ReceptionsInformationByVinNumberResponse[]>(Infrastucture.Properties.Resources.errNotFound, false, null, new List<string> { Infrastucture.Properties.Resources.errNotFound });
        }

        var response = await httpClient.GetFromJsonAsync<ResultDto<ReceptionsInformationByVinNumberResponse[]>>($"Dealer/GetReceptionsInformationByVinNumber?chassisVinNumber={chassisVinNumber}");
        if (response.Data != null)
        {
            return new ResultDto<ReceptionsInformationByVinNumberResponse[]>(Infrastucture.Properties.Resources.msgFound
           , true, response.Data);
        }
        return new ResultDto<ReceptionsInformationByVinNumberResponse[]>(Infrastucture.Properties.Resources.errNotFound, false);
    }
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

    public async Task<ResultDto<BranchesByDealerResponse[]>> GetBranchesByDealerAsync(Guid dealerId)
    {
        var response = await httpClient.GetFromJsonAsync<ResultDto<BranchesByDealerResponse[]>>($"Dealer/GetBranchesByDealer?DealerId={dealerId}");
        if (response.Data != null)
        {
            return new ResultDto<BranchesByDealerResponse[]>(Infrastucture.Properties.Resources.msgFound
           , true, response.Data);
        }
        return new ResultDto<BranchesByDealerResponse[]>(Infrastucture.Properties.Resources.errNotFound, false);
    }
    public async Task<ResultDto<ReceptionsInformationByVinNumberResponse[]>> GetReceptionsInformationByVinNumber()
    {
        var chassisVinNumber = subscriberService.CurrentSubscriber.VehicleModels.FirstOrDefault(c => c.IsDefault)?.VinNumber;
        if (string.IsNullOrEmpty(chassisVinNumber))
        {
            return new ResultDto<ReceptionsInformationByVinNumberResponse[]>(Infrastucture.Properties.Resources.errNotFound, false, null, new List<string> { Infrastucture.Properties.Resources.errNotFound });
        }
        var response = await httpClient.GetFromJsonAsync<ResultDto<ReceptionsInformationByVinNumberResponse[]>>($"Dealer/GetReceptionsInformationByVinNumber?chassisVinNumber={chassisVinNumber}");
        if (response.Data != null)
        {
            return new ResultDto<ReceptionsInformationByVinNumberResponse[]>(Infrastucture.Properties.Resources.msgFound
           , true, response.Data);
        }
        return new ResultDto<ReceptionsInformationByVinNumberResponse[]>(Infrastucture.Properties.Resources.errNotFound, false);
    }
}