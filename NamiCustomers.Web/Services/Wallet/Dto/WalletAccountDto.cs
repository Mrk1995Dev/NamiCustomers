namespace NamiCustomers.Web.Services.Wallet.Dto;

public class WalletAccountDto
{
    public WalletType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public long Balance { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? ExpiresAtPersian { get; set; }
    public bool CanCharge { get; set; }
}
