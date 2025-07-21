using System.Globalization;
using System.Reflection;

namespace NamiCustomers.Infrastucture.Utilities
{
    public static class PersianDateUtility
    {

        private static CultureInfo _culture;
        public static string GetPersianDateString(DateTime? date)
        {
            string PersianDate = "";
            if (!date.HasValue)
            {
                return PersianDate;
            }
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            int Year = p.GetYear(date.Value);
            int Month = p.GetMonth(date.Value);
            int Day = p.GetDayOfMonth(date.Value);
            string MonthName = "فروردین";
            switch (Month)
            {
                case 1: { MonthName = "فروردین"; break; }
                case 2: { MonthName = "اردیبهشت"; break; }
                case 3: { MonthName = "خرداد"; break; }
                case 4: { MonthName = "تیر"; break; }
                case 5: { MonthName = "مرداد"; break; }
                case 6: { MonthName = "شهریور"; break; }
                case 7: { MonthName = "مهر"; break; }
                case 8: { MonthName = "آبان"; break; }
                case 9: { MonthName = "آذر"; break; }
                case 10: { MonthName = "دی"; break; }
                case 11: { MonthName = "بهمن"; break; }
                case 12: { MonthName = "اسفند"; break; }
                default: { MonthName = "فروردین"; break; }
            }
            PersianDate = Day.ToString() + " " + MonthName + " " + Year.ToString();
            return PersianDate;
        }
        public static string GetPersianDateStringSlashed(DateTime? date)
        {
            string PersianDate = "";
            if (!date.HasValue)
            {
                return PersianDate;
            }
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            int Year = p.GetYear(date.Value);
            int Month = p.GetMonth(date.Value);
            int Day = p.GetDayOfMonth(date.Value);

            PersianDate = Day.ToString() + "/" + Month.ToString() + "/" + Year.ToString();
            return PersianDate;
        }
        public static CultureInfo GetPersianCulture()
        {
            if (_culture == null)
            {
                _culture = new CultureInfo("fa-IR");
                DateTimeFormatInfo formatInfo = _culture.DateTimeFormat;
                formatInfo.AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
                formatInfo.DayNames = new[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنجشنبه", "جمعه", "شنبه" };
                var monthNames = new[]
                {
                        "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن",
                        "اسفند",
                        ""
                    };
                formatInfo.AbbreviatedMonthNames =
                    formatInfo.MonthNames =
                    formatInfo.MonthGenitiveNames = formatInfo.AbbreviatedMonthGenitiveNames = monthNames;
                formatInfo.AMDesignator = "ق.ظ";
                formatInfo.PMDesignator = "ب.ظ";
                formatInfo.ShortDatePattern = "yyyy/MM/dd";
                formatInfo.LongDatePattern = "dddd, dd MMMM,yyyy";
                formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;
                Calendar cal = new PersianCalendar();

                FieldInfo fieldInfo = _culture.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
                if (fieldInfo != null)
                    fieldInfo.SetValue(_culture, cal);

                FieldInfo info = formatInfo.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
                if (info != null)
                    info.SetValue(formatInfo, cal);

                _culture.NumberFormat.NumberDecimalSeparator = "/";
                _culture.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
                _culture.NumberFormat.NumberNegativePattern = 0;
            }
            return _culture;
        }

        public static string ToPersianDateString(this DateTime date, string format = "yyyy/MM/dd")
        {
            return date.ToString(format, GetPersianCulture());
        }
    }

}
