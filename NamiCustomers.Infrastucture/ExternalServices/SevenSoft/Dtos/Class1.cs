using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

public class PartsPriceByChassisRequest
{
    [DisplayName("شناسه مدل خودرو")]
    public string VehicleModelId { get; set; }
    [DisplayName("شماره قطعه")]
    public string? PartNo { get; set; }
    [DisplayName("شماره شاسی")]
    public string ChassisVinNumber { get; set; }
    [DisplayName("شناسه ملی")]
    public string NationalCodeOrEconomicCode { get; set; }
    [DisplayName("نام قطعه")]
    public string? PartName { get; set; }
    [DisplayName("شماره تامین کننده")]
    public object PartSupplierNo { get; set; }
}

public class PartsPriceByChassisResponse
{
    [DisplayName("شماره ردیف")]
    public int RowNumber { get; set; }
    [DisplayName("شماره قطعه")]
    public string PartNo { get; set; }
    [DisplayName("نام قطعه")]
    public string PartName { get; set; }
    [DisplayName("قیمت های قطعه")]
    public PartsPriceItemResponse[] Prices { get; set; }
    [DisplayName("قیمت قطعه (بدون ارزش افزوده ) ")]
    public float Price { get; set; }

    [DisplayName("قیمت قطعه (با احتساب ارزش افزوده ) ")]
    public float PriceByTax =>Price + ((int)(Price * 10) / 100);
    public object PartSupplierNo { get; set; }
}

public class PartsPriceItemResponse
{
    public int Code { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public bool Active { get; set; }
    public int SubscriberTypeId { get; set; }
}



public class SpOrderingPartSpOrderingCodeResponse
{
    public int Code { get; set; }
    public string UniqueId { get; set; }
    public string SpOrderingId { get; set; }
    public string SpOrderingCode { get; set; }
    public DateTime SpOrderingDate { get; set; }
    public string StrSpOrderingDate { get; set; }
    public string StrSpOrderingTime { get; set; }
    public int DealerSystemCode { get; set; }
    public string PartGroupId { get; set; }
    public object PartGroupNo { get; set; }
    public object PartGroupName { get; set; }
    public string PartId { get; set; }
    public string PartNo { get; set; }
    public string PartName { get; set; }
    public object ProviderLocalizedCode { get; set; }
    public float OrderNumber { get; set; }
    public float MaxOrderNumber { get; set; }
    public float OrderRate { get; set; }
    public object Inventory { get; set; }
    public float ReceiptNumber { get; set; }
    public float AverageNumber { get; set; }
    public object DailyAverageOrderNumberBasedOnWeek { get; set; }
    public float PreviousOrderNumber { get; set; }
    public float UnitPrice { get; set; }
    public float Tax { get; set; }
    public float Toll { get; set; }
    public float TotalPrice { get; set; }
    public bool DoNotWantSimilarParts { get; set; }
    public object PartRequestTransactionId { get; set; }
    public string SubscriberId { get; set; }
    public string ChassisInfoId { get; set; }
    public object ReceptionId { get; set; }
    public int SpOrderingTypeId { get; set; }
    public string SpOrderingType { get; set; }
    public object ReceptionCode { get; set; }
    public string VinNumber { get; set; }
    public object SpExchangeDate { get; set; }
    public string StrSpExchangeDate { get; set; }
    public object SpExchangeCode { get; set; }
    public DateTime SentDate { get; set; }
    public string StrSentDate { get; set; }
    public string CreatedOnTime { get; set; }
    public string OrderStatus { get; set; }
    public string VehicleName { get; set; }
    public string BackOrderCode { get; set; }
    public object PartUsageCategoryId { get; set; }
    public object PartUsageCategoryName { get; set; }
    public float Discount { get; set; }
    public float DiscountPercent { get; set; }
    public object CountingUnitLocalizedName { get; set; }
    public object PartTypeName { get; set; }
    public object PartTypeId { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}





public class SpOrderingsBySubscriberResponse
{
    public string TrackCode => $"{DealerNo}-{BranchNo}-{DealerSystemCode}";
    public int Code { get; set; }
    public string UniqueId { get; set; }
    public int DealerSystemCode { get; set; }
    public int BranchSystemCode { get; set; }
    public string DealerId { get; set; }
    public string DealerNo { get; set; }
    public object DealerName { get; set; }
    public string BranchId { get; set; }
    public string BranchNo { get; set; }
    public object BranchName { get; set; }
    public object SporderingCode { get; set; }
    public string SubscriberChassisAllocationId { get; set; }
    public object SubscriberId { get; set; }
    public string SubscriberName { get; set; }
    public object SubscriberNameWithoutNationalCode { get; set; }
    public object SubscriberMobile { get; set; }
    public object ChassisPlate { get; set; }
    public object ReceptionId { get; set; }
    public object ReceptionCode { get; set; }
    public object WarrantyStatus { get; set; }
    public int ReceptionDealerSystemCode { get; set; }
    public string RegisterPersonnelId { get; set; }
    public string PersonnelName { get; set; }
    public int OrderTypeId { get; set; }
    public string OrderTypeLocalizedName { get; set; }
    public string RelatedSystemCode { get; set; }
    public string OrderPriorityTypeId { get; set; }
    public string OrderPriorityTypeLocalizedName { get; set; }
    public object OrderReceiveTypeId { get; set; }
    public string OrderReceiveTypeLocalizedName { get; set; }
    public object OrderStatusTypeId { get; set; }
    public string OrderStatusTypeLocalizedName { get; set; }
    public string PaymentTypeId { get; set; }
    public string PaymentTypeLocalizedName { get; set; }
    public string DestinationDealerId { get; set; }
    public string DestinationBranchId { get; set; }
    public bool IsBackOrder { get; set; }
    public bool Sent { get; set; }
    public DateTime SentDate { get; set; }
    public string StrSentDate { get; set; }
    public string StrSentTime { get; set; }
    public bool Status { get; set; }
    public string StatusDescription { get; set; }
    public float TotalFee { get; set; }
    public float Discount { get; set; }
    public float DiscountPercent { get; set; }
    public float Tax { get; set; }
    public float Toll { get; set; }
    public float EndFee { get; set; }
    public object[] SpOrderingParts { get; set; }
    public DateTime Date { get; set; }
    public string StrDate { get; set; }
    public string StrTime { get; set; }
    public string SpExchangeId { get; set; }
    public string SpOrderingSourceWareHouseId { get; set; }
    public string SpOrderingSourceWareHouse { get; set; }
    public string VinNumber { get; set; }
    public object PartNo { get; set; }
    public object PartLocalizedName { get; set; }
    public object CustomerMobile { get; set; }
    public object CustomerEmail { get; set; }
    public string ChassisNo { get; set; }
    public object PartConsumptionTypeId { get; set; }
    public object PartConsumptionTypeName { get; set; }
    public object CargoVehicleTypeId { get; set; }
    public object CargoVehicleTypeName { get; set; }
    public object SpExchangesDealerFactorCode { get; set; }
    public object SpExchangesDealerYearFactorCode { get; set; }
    public object SpExchangesBranchFactorCode { get; set; }
    public object SpExchangesBranchYearFactorCode { get; set; }
    public object SpExchangesBranchCollectiveInvoiceNumberByYear { get; set; }
    public object SpExchangesDealerCollectiveInvoiceNumber { get; set; }
    public object SpOrderingListVmSpecialSubscriberGroupName { get; set; }
    public object SpOrderingListVmCityName { get; set; }
    public object SpOrderingListVmVehicleModelName { get; set; }
    public object SpOrderingListVmReceiptionStateOrder { get; set; }
    public object SpOrderingListVmReceptionRecKilometer { get; set; }
    public object SpOrderingListVmTechnicianName { get; set; }
    public object SpOrderingListVmDescription { get; set; }
    public object PartTypeId { get; set; }
    public object PartType { get; set; }
    public object PartName { get; set; }
    public object CountingUnitLocalizedName { get; set; }
    public float UnitPrice { get; set; }
    public int Number { get; set; }
    public object TemporaryReceiptDealerNo { get; set; }
    public int TemporaryReceiptDealerSystemCode { get; set; }
    public object SpexchangesStateOrderId { get; set; }
    public object State { get; set; }
    public int Gender { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}




 

public class AllOrderStatusTypeResponse
{
    public int Code { get; set; }
    public string UniqueId { get; set; }
    public string OrderStatusTypeLocalizedName { get; set; }
    public string OrderStatusTypeName { get; set; }
    public object Description { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}
