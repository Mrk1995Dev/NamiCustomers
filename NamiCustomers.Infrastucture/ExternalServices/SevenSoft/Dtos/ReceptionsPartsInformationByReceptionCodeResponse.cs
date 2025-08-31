
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
 
public class ReceptionsPartsInformationByReceptionCodeResponse
{
    public int Code { get; set; }
    public string UniqueId { get; set; }
    public string ReceptionId { get; set; }
    public int ReceptionSystemDealerCode { get; set; }
    public string ReceptionDealerNo { get; set; }
    public string ReceptionBranchNo { get; set; }
    public string PartId { get; set; }
    [DisplayName("کد قطعه")]
    public string PartNo { get; set; }
    public string OldPartNo { get; set; }
    public string PartProviderLocalizedCode { get; set; }
	[DisplayName("نام لاتین قطعه")]
	public string Part { get; set; }
	[DisplayName("نام قطعه ")]
	public string PartName { get; set; }
    public int? PartTypeEnumCode { get; set; }
	[DisplayName("مبلغ واحد")]
	[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
	public float UnitPrice { get; set; }
    public float ExtraPrice { get; set; }
	[DisplayName("تخفیف")]
	[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
	public float Discount { get; set; }
    public float DiscountValue { get; set; }
    public float DiscountPercent { get; set; }
	[DisplayName("مقدار")]
	[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
	public float Number { get; set; }
	[DisplayName("مبلغ کل")]
	[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
	public float TotalPrice { get; set; }
    public bool HasWarranty { get; set; }
    public string CostCenterId { get; set; }
	[DisplayName("مرکز هزینه")]
	public string CostCenterName { get; set; }
    public string CostCenter { get; set; }
	[DisplayName("تعداد")]
	public string PartUnit { get; set; }
    public string ReceptionRowVersion { get; set; }
    public bool IsPayableForCustomer { get; set; }
    public bool PartIsNonCompany { get; set; }
    public string ParentPartId { get; set; }

	[DisplayName("جمع مالیات و عوارض")]
	[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
	public float Tax { get; set; }
    public float Toll { get; set; }
    public string ValidGuarantyCostCenter { get; set; }
    public bool AutomaticClaimRegister { get; set; }
    public string ReceptionCode { get; set; }
    public DateTime RecDate { get; set; }
    public string StrRecDate { get; set; }
    public int RecKilometer { get; set; }
    public string RecClock { get; set; }
    public string ReceptionPartsShippingTypeId { get; set; }
    public string ExitDate { get; set; }
    public string StrExitDate { get; set; }
    public string SpOrderingCode { get; set; }
    public string StrCreatedOn { get; set; }
    public string PackageId { get; set; }
    public bool DontFailureLicense { get; set; }
    public string Description { get; set; }
    public bool FromReceptionTask { get; set; }
    public float TaxForShow { get; set; }
    public float TollForShow { get; set; }
    public bool IsNotEditable { get; set; }
    public string PartName_En { get; set; }
    public bool SafetyPart { get; set; }
    public string[] Discounts { get; set; }
    public string PartGroupName { get; set; }
    public string PartTypeName { get; set; }
    public string PartTypeId { get; set; }
    public int CostCenterCode { get; set; }
    public string PartSerialId { get; set; }
    public string PartSerial { get; set; }
    public string ClaimProblemReasonId { get; set; }
    public string PartCatalog { get; set; }
    public string IDCode { get; set; }
    public string CountingUnitCatalog { get; set; }
    public string PartGroupNo { get; set; }
    public string WarehouseId { get; set; }
    public bool ServicePack { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}
