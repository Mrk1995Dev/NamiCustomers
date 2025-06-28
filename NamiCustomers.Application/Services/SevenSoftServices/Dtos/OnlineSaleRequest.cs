namespace NamiCustomers.Application.Services.SevenSoftServices.Dtos;

public class OnlineSaleRequest
{
    public long? AccountingFee { get; set; }
    public string? FollowUpCode { get; set; }
    public int? FundCode { get; set; }
    public int? DestinationBankAccountNumberCode { get; set; }
    public string? UniqueId { get; set; }
    public string? SalePaymentKindId { get; set; }
    public string? PaymentIdLogId { get; set; }
}
