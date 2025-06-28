namespace NamiCustomers.Application.Services.SevenSoftServices.Dtos;

public class InsertOnlineSaleRequest
{
    public Guid SubscriberId { get; set; }
    public Guid SalePlanId { get; set; }
    public Guid SaleBasketId { get; set; }
    public Guid VehicleModelId { get; set; }
    public Guid BodyColorDetailId { get; set; }
    public Guid InternalColorDetailId { get; set; }
    public int? HarmonicColorDetailId { get; set; }
    public int? ChassisInfoId { get; set; }
    public Guid SaleBasketInventoryControlId { get; set; }
    public Guid SalePlanConditionId { get; set; }
    public int SalePaymentTermId { get; set; }
    public int? VehicleModelSalePriceId { get; set; }
    public string Description { get; set; }
    public int AreaCategoryId { get; set; }
    public Guid VehicleSaleFollowupDealerId { get; set; }
    public Guid VehicleSaleFollowupBranchId { get; set; }
    public Guid? VehicleUsageTypeId { get; set; }
    public Guid BrandId { get; set; }
    public Guid FirstColorPriorityId { get; set; }
    public Guid SecondColorPriorityId { get; set; }
    public Guid ThirdColorPriorityId { get; set; }
    public Guid? ClientId { get; set; }
    public bool IsDirty { get; set; }
}
