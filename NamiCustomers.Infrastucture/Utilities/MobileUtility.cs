using System.Text.RegularExpressions;

namespace NamiCustomers.Infrastucture.Utilities;

public static class MobileUtility
{
    /// <summary>
    /// اضافه کرد +98 به ابتدای موبایل
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    public static string To98(this string mobile)
    {
        return  "98" + mobile.Substring(mobile.Length - 10);
    }
    /// <summary>
    /// شماره موبایل ایرانی معتبراست
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    public static bool IsValidIranianMobileNumber(this string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        // Remove any whitespace
        phoneNumber = phoneNumber.Trim();

        // Pattern for Iranian mobile numbers
        string pattern = @"^(?:\+98|0098|0)?9[0-9]{9}$";

        return Regex.IsMatch(phoneNumber, pattern);
    }

}
