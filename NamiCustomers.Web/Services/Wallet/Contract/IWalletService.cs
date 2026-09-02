using NamiCustomers.Abstractions.Dtos;
using NamiCustomers.Web.Services.Wallet.Dto;

namespace NamiCustomers.Web.Services.Wallet.Contract;

public interface IWalletService
{
    Task<ResultDto<WalletOverviewDto>> GetOverviewAsync();
    Task<ResultDto<WalletOverviewDto>> ChargeTomanAsync(long amount);
}
