namespace NamiCustomers.Web.Services.Vehicle.Dto;

public class SpOrderingsBySubscriberDto
{
    public int Code { get; set; }
    public string? UniqueId { get; set; }
    public int DealerSystemCode { get; set; }
    public string? DealerNo { get; set; }
    public string? BranchNo { get; set; }
    public object? DealerName { get; set; }
    public object? BranchName { get; set; }
    public object? SporderingCode { get; set; }
    public string? SubscriberName { get; set; }
    public string? PersonnelName { get; set; }
    public string? OrderTypeLocalizedName { get; set; }
    public string? OrderStatusTypeLocalizedName { get; set; }
    public string? OrderReceiveTypeLocalizedName { get; set; }
    public string? PaymentTypeLocalizedName { get; set; }
    public string? OrderPriorityTypeLocalizedName { get; set; }
    public string? StatusDescription { get; set; }
    public bool Sent { get; set; }
    public string? StrSentDate { get; set; }
    public string? StrSentTime { get; set; }
    public float TotalFee { get; set; }
    public float Discount { get; set; }
    public float EndFee { get; set; }
    public string? StrDate { get; set; }
    public string? StrTime { get; set; }
    public string? VinNumber { get; set; }
    public string? ChassisNo { get; set; }
}
