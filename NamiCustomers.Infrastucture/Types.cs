using System.ComponentModel;

public enum BankcodeType : int
{
    Unknown = 0,
    Gardeshgari = 1,
    Ayandeh = 2
}
public enum BankCodeTypeOfSevenSoft : int
{
    Gardeshgari = 8,
    Ayandeh = 6
}
public enum FundCodeTypeOfSevenSoft : int
{
    Sandogh = 35
}
public enum AttachmentStatusType : int
{
    WaitToConfirm = 0,
    Confirm = 1,
}

public enum PaymentType
{
    PrePayment = 1, //1 => پیش پرداخت
    CompletionPayment = 2  //2=> تکمیل وجه
}

public enum SaleStatusType
{
    [Description("تکمیل اطلاعات تکمیل اطلاعات")]
    FillBiography = 1,
    [Description("تاییدیه کاتالوگ")]
    ConfirmCatalog = 2,
    [Description("بخشنامه فروش")]
    SalesAnnonce = 3,
    [Description("رنگ پیشنهادی")]
    SuggestColor = 6,
    [Description("انتخاب عاملیت")]
    SelectAgent = 8,
    [Description("پرداخت")]
    SalePayment = 9,
    [Description("وضعیت پرداخت")]
    UploadDocs = 10,
    [Description("تکمیل وجه")]
    CompletionPayment = 11,
    [Description("آپلود مدارک")]
    Attachment = 12
}

public enum MyClaims
{
    UserId,
    Mobile,
    NationalCode,
    FullName,
    Role,
    PersianRole
}

public static class MyRoles
{
    public static string Admin => "Admin";
    public static string Operator => "Operator";
    public static string Subscriber => "Subscriber";
}
public  class MyPloicies
{
    public static string AdminAccess => "AdminAccess";
    public static string OperatorAccess => "OperatorAccess";
    public static string SubscriberAccess=> "SubscriberAccess";
}
