namespace NamiCustomers.Infrastucture.Utilities;

public static  class TypeConvertUtility
{
    public static Guid ToGuid(this string id)
    {
        return Guid.Parse(id);
    }
}
