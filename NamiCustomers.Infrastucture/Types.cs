
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
