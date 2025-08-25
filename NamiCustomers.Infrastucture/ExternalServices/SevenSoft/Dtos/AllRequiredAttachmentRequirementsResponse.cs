namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;


public class AllRequiredAttachmentRequirementsResponse
{
    public string UniqueId { get; set; }
    public int Code { get; set; }
    public string VehicleSaleId { get; set; }
    public int VehicleSaleCode { get; set; }
    public string VehicleSaleNumber { get; set; }
    public string AttachmentRequirementId { get; set; }
    public string AttachmentRequirement { get; set; }
    public string Description { get; set; }
    public bool Required { get; set; }
    public object StatusId { get; set; }
    public string Status { get; set; }
    public bool IsSent { get; set; }
    public object CreatedOn { get; set; }
    public string DealerId { get; set; }
    public object DealerNo { get; set; }
    public object DealerName { get; set; }
    public string BranchId { get; set; }
    public object BranchNo { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}
