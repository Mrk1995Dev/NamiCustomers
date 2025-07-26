using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;

namespace NamiCustomers.Application.Services.Dealers;

public interface IDealerService
{
    Task<ResultDto<DealerResponse[]>> GetDealersAsync();
}
public class DealerService(IMapper mapper,ISevenSoftService sevenSoftService) : IDealerService
{
    public async Task<ResultDto<DealerResponse[]>> GetDealersAsync()
    {
        var data =await  sevenSoftService.GetDealers();
        var models = mapper.Map<DealerResponse[]>(data);
        return new ResultDto<DealerResponse[]>(Infrastucture.Properties.Resources.msgFound, true, models);
    }
}
