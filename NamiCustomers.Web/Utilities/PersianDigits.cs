namespace NamiCustomers.Web.Utilities;

public static class PersianDigits
{
    public static string ToEnglish(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        var chars = new char[value.Length];
        var count = 0;

        foreach (var c in value)
        {
            if (c is >= '0' and <= '9')
            {
                chars[count++] = c;
            }
            else if (c is >= '\u06F0' and <= '\u06F9')
            {
                chars[count++] = (char)('0' + (c - '\u06F0'));
            }
            else if (c is >= '\u0660' and <= '\u0669')
            {
                chars[count++] = (char)('0' + (c - '\u0660'));
            }
        }

        return new string(chars, 0, count);
    }

    public static string ToPersian(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        var chars = value.ToCharArray();
        for (var i = 0; i < chars.Length; i++)
        {
            if (chars[i] is >= '0' and <= '9')
            {
                chars[i] = (char)('\u06F0' + (chars[i] - '0'));
            }
            else if (chars[i] is >= '\u0660' and <= '\u0669')
            {
                chars[i] = (char)('\u06F0' + (chars[i] - '\u0660'));
            }
        }

        return new string(chars);
    }
}
