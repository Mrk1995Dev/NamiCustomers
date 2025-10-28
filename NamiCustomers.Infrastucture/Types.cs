
using System.ComponentModel;

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
public class MyPloicies
{
    public static string AdminAccess => "AdminAccess";
    public static string OperatorAccess => "OperatorAccess";
    public static string SubscriberAccess => "SubscriberAccess";
}


public enum SubscriberType
{
    [Description("حقیقی")]
    Haghighi = 1,
    [Description("حقوقی")]
    Hogooghi = 2
}

public enum GenderType
{
    [Description("آقا")]
    Male=1,
    [Description("خانم")]
    Female =2,
}