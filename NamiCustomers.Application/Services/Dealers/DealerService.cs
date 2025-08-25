using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

namespace NamiCustomers.Application.Services.Dealers;

public interface IDealerService
{
    Task<ResultDto<BranchesByDealerResponse[]>> GetBranchesByDealerAsync(Guid dealerId);
    Task<ResultDto<DealerResponse[]>> GetDealersAsync();
    Task<ResultDto<ReceptionsInformationByVinNumberResponse[]>> GetReceptionsInformationByVinNumber(string chassisVinNumber);
}
public class DealerService(IMapper mapper, ISevenSoftService sevenSoftService) : IDealerService
{
    public async Task<ResultDto<BranchesByDealerResponse[]>> GetBranchesByDealerAsync(Guid dealerId)
    {
        var data = await sevenSoftService.GetBranchesByDealer(dealerId);
        var models = mapper.Map<BranchesByDealerResponse[]>(data);
        return new ResultDto<BranchesByDealerResponse[]>(Infrastucture.Properties.Resources.msgFound, true, models);
    }

    public async Task<ResultDto<DealerResponse[]>> GetDealersAsync()
    {
        var data = await sevenSoftService.GetDealers();
        var models = mapper.Map<DealerResponse[]>(data);
        return new ResultDto<DealerResponse[]>(Infrastucture.Properties.Resources.msgFound, true, models);
    }

    public async Task<ResultDto<ReceptionsInformationByVinNumberResponse[]>> GetReceptionsInformationByVinNumber(string chassisVinNumber)
    {
        var data = await sevenSoftService.GetReceptionsInformationByVinNumber(chassisVinNumber);
        var models = mapper.Map<ReceptionsInformationByVinNumberResponse[]>(data);
        return new ResultDto<ReceptionsInformationByVinNumberResponse[]>(Infrastucture.Properties.Resources.msgFound, true, models);
    }
}
