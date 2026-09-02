namespace NamiCustomers.Web.Services.Wallet.Dto;

public class WalletTransactionDto
{
    public string Id { get; set; } = string.Empty;
    public WalletType WalletType { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public long Amount { get; set; }
    public string DatePersian { get; set; } = string.Empty;
    public string TimePersian { get; set; } = string.Empty;
    public bool IsCredit => Amount > 0;
}
