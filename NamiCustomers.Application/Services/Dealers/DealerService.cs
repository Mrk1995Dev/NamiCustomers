using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

namespace NamiCustomers.Application.Services.Dealers;

public interface IDealerService
{
    Task<ResultDto<GetBranchesByDealerResponse[]>> GetBranchesByDealerAsync(Guid dealerId);
    Task<ResultDto<DealerResponse[]>> GetDealersAsync();
    Task<ResultDto<GetReceptionsInformationByVinNumberResponse[]>> GetReceptionsInformationByVinNumber(string chassisVinNumber);
}
public class DealerService(IMapper mapper, ISevenSoftService sevenSoftService) : IDealerService
{
    public async Task<ResultDto<GetBranchesByDealerResponse[]>> GetBranchesByDealerAsync(Guid dealerId)
    {
        var data = await sevenSoftService.GetBranchesByDealer(dealerId);
        var models = mapper.Map<GetBranchesByDealerResponse[]>(data);
        return new ResultDto<GetBranchesByDealerResponse[]>(Infrastucture.Properties.Resources.msgFound, true, models);
    }

    public async Task<ResultDto<DealerResponse[]>> GetDealersAsync()
    {
        var data = await sevenSoftService.GetDealers();
        var models = mapper.Map<DealerResponse[]>(data);
        return new ResultDto<DealerResponse[]>(Infrastucture.Properties.Resources.msgFound, true, models);
    }

    public async Task<ResultDto<GetReceptionsInformationByVinNumberResponse[]>> GetReceptionsInformationByVinNumber(string chassisVinNumber)
    {
        var data = await sevenSoftService.GetReceptionsInformationByVinNumber(chassisVinNumber);
        var models = mapper.Map<GetReceptionsInformationByVinNumberResponse[]>(data);
        return new ResultDto<GetReceptionsInformationByVinNumberResponse[]>(Infrastucture.Properties.Resources.msgFound, true, models);
    }
}
