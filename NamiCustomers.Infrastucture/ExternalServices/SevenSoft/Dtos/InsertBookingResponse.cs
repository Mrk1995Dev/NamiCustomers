
public class InsertBookingResponse
{
    public int AddStatus { get; set; }
    public string ReturnKey { get; set; }
    public string Message { get; set; }
    public Returnmodel ReturnModel { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}

public class Returnmodel
{
    public Customerstatement[] CustomerStatements { get; set; }
    public string VinNumber { get; set; }
    public string BookingCode { get; set; }
    public string DealerNo { get; set; }
    public string BranchNo { get; set; }
    public string DealerName { get; set; }
    public string BranchName { get; set; }
    public object DealerAddress { get; set; }
    public object DealerLatitude { get; set; }
    public object DealerLongitude { get; set; }
    public int DealerSystemCode { get; set; }
    public object DealerPhone { get; set; }
    public string DealerCity { get; set; }
    public object DealerServerGroup { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime BookingDate { get; set; }
    public int BranchSystemCode { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}

public class Customerstatement
{
    public string BookingCustomerStatementDescription { get; set; }
    public string CustomerStatementsTypeId { get; set; }
    public string DefaultCustomerDescriptionId { get; set; }
    public object DefaultCustomerDescription { get; set; }
    public bool Approved { get; set; }
    public string CustomerStatementsTypeName { get; set; }
    public string DefaultCustomerDescriptionName { get; set; }
}
