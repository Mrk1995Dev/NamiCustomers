using NamiCustomers.Web.Services.Common.Dto;
using NamiCustomers.Web.Services.Dealer.Dto;


namespace NamiCustomers.Web.Services.Dealer.Contract;

public interface IDealerService
{
    Task<ResultDto<DealerResponseDto[]>> GetAllDealerAsync();
    Task<ResultDto<BranchesByDealerResponse[]>> GetAllBranchesByDealerAsync(Guid dealerId);
}