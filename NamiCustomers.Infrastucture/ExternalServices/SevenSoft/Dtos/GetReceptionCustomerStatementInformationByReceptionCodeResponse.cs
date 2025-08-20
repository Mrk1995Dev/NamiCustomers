using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

public class GetReceptionCustomerStatementInformationByReceptionCodeResponse
{
    public int Code { get; set; }
    public string UniqueId { get; set; }
    public string ReceptionId { get; set; }
    public int PresentReceptionDealerSystemCode { get; set; }
    public string ReturnReceptionCode { get; set; }
    public object ReturnReceptionId { get; set; }
    public string CustomerStatementsTypeId { get; set; }
    public string DefaultCustomerDescriptionId { get; set; }
    public string DefaultCustomerDescriptionLocalizedName { get; set; }
    public string CustomerStatementsTypeLocalizedName { get; set; }
    public int StatementsTypeId { get; set; }
    public string StatementsTypeLocalizedName { get; set; }
    public string CustomerDescription { get; set; }
    public string ExpertTheory { get; set; }
    public bool Approved { get; set; }
    public object ReceptionDealerSystemCode { get; set; }
    public object PropblemFinderPersonnelId { get; set; }
    public string PropblemFinderPersonnelName { get; set; }
    public string ReceptionRowVersion { get; set; }
    public object BookingCustomerStatementId { get; set; }
    public string ReceptionCode { get; set; }
    public int RecKilometer { get; set; }
    public DateTime RecDate { get; set; }
    public string StrRecDate { get; set; }
    public string ReceptionCustomerStatementServerId { get; set; }
    public bool Safety { get; set; }
    public float EstimatedTime { get; set; }
    public object UserCode { get; set; }
    public object SafetyUserApproverId { get; set; }
    public bool CheckingSafety { get; set; }
    public object SafetyApprovedDate { get; set; }
    public object SafetyDescription { get; set; }
    public string StrSafetyApprovedDate { get; set; }
    public object SafetyUserApprover { get; set; }
    public bool ReluctanceToReceiveAlternateVehicle { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}
