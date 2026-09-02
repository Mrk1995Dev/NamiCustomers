namespace NamiCustomers.Web.Services.Wallet.Dto;

public class WalletOverviewDto
{
    public List<WalletAccountDto> Accounts { get; set; } = [];
    public List<WalletTransactionDto> Transactions { get; set; } = [];
    public long TotalBalance => Accounts.Sum(account => account.Balance);
}
