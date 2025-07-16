namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
public class AddOrUpdatevehicleSaleAttachmentRequest
{
    public Vehiclesaleattachment VehicleSaleAttachment { get; set; }
    public Addfile[] AddFile { get; set; }
}

public class Vehiclesaleattachment
{
    public string UniqueId { get; set; }
    public int Code { get; set; }
    public string VehicleSaleId { get; set; }
    public string AttachmentRequirementId { get; set; }
    public string Description { get; set; }
    public bool FromVehicleSaleGroup { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}

public class Addfile
{
    public string UniqueId { get; set; }
    public string ForeignUniqueId { get; set; }
    public string AttachmentTitle { get; set; }
    public string FileName { get; set; }
    public string Extension { get; set; }
    public string FilePath { get; set; }
    public bool Deleted { get; set; }
    public string Content { get; set; }
    public string Thumbnail { get; set; }
    public string ReturnUrl { get; set; }
    public string ModifiedUserId { get; set; }
    public int BusinessTypeId { get; set; }
    public string GridName { get; set; }
    public string AttachmentRequirementId { get; set; }
    public bool IsTemp { get; set; }
    public int MaximumFileStorageVolumeInTheSystem { get; set; }
    public Uploadeditem[] UploadedItems { get; set; }
    public bool fromVehicleSaleEditDelivery { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}

public class Uploadeditem
{
    public string UniqueId { get; set; }
    public int Code { get; set; }
    public string ForeignUniqueId { get; set; }
    public string FileName { get; set; }
    public string Extension { get; set; }
    public string Title { get; set; }
    public string Thumbnail { get; set; }
    public string Content { get; set; }
    public DateTime Now { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedOnDate { get; set; }
    public string CreatedOnTime { get; set; }
    public string User { get; set; }
    public string AttachmentRequirementId { get; set; }
    public string VehicleSaleAttachmentId { get; set; }
    public string AttachmentRequirementTitle { get; set; }
    public bool IsNotEditable { get; set; }
    public bool FromHistory { get; set; }
    public string ChassisInfoId { get; set; }
    public string CreatedPersonalName { get; set; }
    public string FilePath { get; set; }
    public string UserId { get; set; }
}
 