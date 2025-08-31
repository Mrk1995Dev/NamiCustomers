using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

public class ReceptionsOutServicesInformationByReceptionCodeResponse
{
    public string UniqueId { get; set; }
    public int Code { get; set; }
    public string ReceptionId { get; set; }
    public int ReceptionSystemDealerCode { get; set; }
    public string ReceptionDealerNo { get; set; }
    public string ReceptionBranchNo { get; set; }
    public string ServiceId { get; set; }
	[DisplayName("کد خدمت")]    
	public string ServiceCode { get; set; }
	[DisplayName("نام خدمت")]
	public string ServiceName { get; set; }
    public object PersonnelId { get; set; }
    public object PersonnelLocalizedName { get; set; }
    public object ServiceGroupId { get; set; }
    public object ServiceGroupLocalizedName { get; set; }
	[DisplayName("مبلغ واحد(ریال)")]
	[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
	public float UnitPrice { get; set; }
    public float ExtraPrice { get; set; }
	[DisplayName("تخفیف")]
	[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
	public float Discount { get; set; }
    public float DiscountValue { get; set; }
    public float DiscountPercent { get; set; }
	[DisplayName("تعداد")]
	[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
	public float Number { get; set; }
	[DisplayName("مبغ کل")]
	[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
	public float TotalPrice { get; set; }
    public bool HasWarranty { get; set; }
    public bool SpecialServices { get; set; }
    public bool ServicePack { get; set; }
    public object PackageId { get; set; }
    public object PackageLocalizedName { get; set; }
    public string CostCenterId { get; set; }
	[DisplayName("مرکز هزینه")]
	public string CostCenterLocalizedName { get; set; }
    public string ReceptionRowVersion { get; set; }
    public bool IsPayableForCustomer { get; set; }
    public float ReturnAvailableNumber { get; set; }
    public bool PriceIsEditableByUser { get; set; }
    public float Time { get; set; }
    public bool AutomaticClaimRegister { get; set; }
    public string ReceptionCode { get; set; }
    public DateTime RecDate { get; set; }
    public string StrRecDate { get; set; }
    public int RecKilometer { get; set; }
	[DisplayName("مالیات")]
	[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
	public float Tax { get; set; }
    public float Toll { get; set; }
    public float TaxForShow { get; set; }
    public float TollForShow { get; set; }
    public bool IsOutService { get; set; }
    public object ClaimProblemReasonId { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}

 