namespace SRH.Utilities.EhsaN;

public class PersianCalender
{
    DateTime _BaseDate = DateTime.MinValue;
    public static DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    static NewPersianCalendar pCal = new NewPersianCalendar();
    static string[] PersianMonthNames
    {
        get
        {
            return new string[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
        }
    }
    public static System.Collections.Generic.List<string> PersianMonths
    {
        get
        {
            var ret = new System.Collections.Generic.List<string>();
            for (int i = 0; i < PersianMonthNames.Length; i++)
            {
                ret.Add(PersianMonthNames[i]);
            }
            return ret;
        }
    }
    public PersianCalender(DateTime baseDate)
    {
        this._BaseDate = baseDate;
    }

    public static string GetCurrentPersianDate(string split)
    {
        return GetPersianDate(DateTime.Now, split);
    }
    public static string GetCurrentStringPersianDate()
    {
        string dateNow = DateTime.Now.ToLongDateString();
        string strDate = PersianCalender.GetPersianDay(Convert.ToDateTime(dateNow)).ToString() + " ";
        strDate += PersianCalender.GetPersianMonthName(Convert.ToDateTime(dateNow)).ToString() + " ";
        return strDate += (PersianCalender.GetPersianYear(Convert.ToDateTime(dateNow)).ToString()).Remove(0, 2);
    }
    public static string GetCurrentFullStringPersianDate()
    {
        string dateNow = DateTime.Now.ToLongDateString();
        string strDate = PersianCalender.GetPersianDay(Convert.ToDateTime(dateNow)).ToString() + " ";
        strDate += PersianCalender.GetPersianMonthName(Convert.ToDateTime(dateNow)).ToString() + " ";
        return strDate += (PersianCalender.GetPersianYear(Convert.ToDateTime(dateNow)).ToString());
    }
    public static string GetPersianDate(DateTime date, string split)
    {
        if (string.IsNullOrEmpty(split)) split = string.Empty;
        return string.Format("{1}{0}{2}{0}{3}", split,
                pCal.GetYear(date),
                (pCal.GetMonth(date).ToString().Length < 2) ? "0" + pCal.GetMonth(date).ToString() : pCal.GetMonth(date).ToString(),
                (pCal.GetDayOfMonth(date).ToString().Length < 2) ? "0" + pCal.GetDayOfMonth(date).ToString() : pCal.GetDayOfMonth(date).ToString());
    }
    public static string GetPersianDate(object date)
    {
        if (date == null) return string.Empty;
        return GetPersianDate(Convert.ToDateTime(date), "/");
    }
    public static DateTime AddPersianMonths(DateTime date, int months)
    {
        return pCal.AddMonths(date, months);
    }

    static string[] PersianDayNames
    {
        get
        {
            //return new string[] { "جمعه", "شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنج شنبه" };
            return new string[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه", "شنبه" };
        }
    }
    public static string GetPersianDayNames(DateTime date)
    {
        return PersianDayNames[(int)date.DayOfWeek];
    }
    public static int GetPersianDay(DateTime date)
    {
        return pCal.GetDayOfMonth(date);
    }
    public static string GetPersianDayName(DateTime date)
    {
        return GetPersianDayName(pCal.GetDayOfWeek(date));
    }
    public static string GetPersianDayName(DayOfWeek day)
    {
        string dayName = string.Empty;
        switch (day)
        {
            case (DayOfWeek.Friday):
                dayName = "جمعه";
                break;
            case (DayOfWeek.Monday):
                dayName = "دوشنبه";
                break;
            case (DayOfWeek.Saturday):
                dayName = "شنبه";
                break;
            case (DayOfWeek.Sunday):
                dayName = "یکشنبه";
                break;
            case (DayOfWeek.Thursday):
                dayName = "پنج شنبه";
                break;
            case (DayOfWeek.Tuesday):
                dayName = "سه شنبه";
                break;
            case (DayOfWeek.Wednesday):
                dayName = "چهار شنبه";
                break;
        }
        return dayName;
    }

    public static int GetPersianMonth(DateTime date)
    {
        return pCal.GetMonth(date);
    }
    public static string GetPersianMonthName(DateTime date)
    {
        return GetPersianMonthName(pCal.GetMonth(date));
    }
    public static string GetPersianMonthName(int month)
    {
        return PersianMonthNames[month - 1];
    }

    public static int GetPersianYear(DateTime date)
    {
        return pCal.GetYear(date);
    }

    public static TimeSpan GetCurrentTime(int TimeDifference)
    {
        DateTime date = DateTime.Now;
        date = date.ToUniversalTime().AddMinutes(TimeDifference);
        return date.TimeOfDay;
    }
    public static string GetCurrentTimeString(int TimeDifference, string Splitter)
    {
        string baseTime = GetCurrentTime(TimeDifference).ToString();
        return string.Format("{0}{1}{2}", baseTime.Split(':')[0], Splitter, baseTime.Split(':')[1]);
    }


    public static DateTime ConvertToGeorgianDateTime(object year, object month, object day)
    {
        int y = Convert.ToInt32(year.ConvertToGeorgianNumber());
        int m = Convert.ToInt32(month.ConvertToGeorgianNumber());
        int d = Convert.ToInt32(day.ConvertToGeorgianNumber());
        //return pCal.ToDateTime(y, m, d, 12, 0, 0, 0);
        return pCal.ToDateTime(y, m, d, 0, 0, 0, 0);
    }
    public static DateTime ConvertToGeorgianDateTime(object year, object month, object day, object hour, object minute, object second)
    {
        int y = Convert.ToInt32(year);
        int m = Convert.ToInt32(month);
        int d = Convert.ToInt32(day);
        int h = Convert.ToInt32(hour);
        int min = Convert.ToInt32(minute);
        int s = Convert.ToInt32(second);
        return pCal.ToDateTime(y, m, d, h, min, s, 0);
    }
    public static DateTime ConvertToGeorgianDateTime(string PersianDate)
    {
        DateTime? tmp = null;
        if (!string.IsNullOrEmpty(PersianDate))
        {
            string[] DateParts = PersianDate.Split('/');
            tmp = ConvertToGeorgianDateTime(DateParts[0], DateParts[1], DateParts[2]);
        }
        return Convert.ToDateTime(tmp);
    }
    public static DateTime? ConvertToGeorgianDate(object year, object month, object day)
    {
        try
        {
            int y = Convert.ToInt32(year);
            int m = Convert.ToInt32(month);
            int d = Convert.ToInt32(day);
            return pCal.ToDateTime(y, m, d, 0, 0, 0, 0);
        }
        catch
        {
            return null;
        }
    }

    public static string GetStringDate(DateTime date, bool JustDate = false)
    {
        string Year = string.Empty;
        var datediff = DateTime.Now - date;
        if (!JustDate)
        {
            if (datediff.TotalSeconds < 60) return datediff.Seconds + " ثانیه قبل";
            if (datediff.TotalMinutes < 60) return datediff.Minutes + " دقیقه قبل";
            if (datediff.TotalHours < 24) return datediff.Hours + " ساعت قبل";
        }
        if (GetPersianYear(DateTime.Now) != GetPersianYear(date)) Year = ", " + GetPersianYear(date);
        return GetPersianDay(date) + " " + GetPersianMonthName(date) + Year;
    }

    public static string GetStringTime(DateTime date)
    {
        return date.ToShortTimeString().ToUpper().Replace("AM", "ق.ظ").Replace("PM", "ب.ظ");
    }

    public static string GetTotalStringDateTime(DateTime date)
    {
        return GetPersianDayName(date) + ", " +
                GetPersianDay(date) + " " +
                GetPersianMonthName(date) + " " +
                GetPersianYear(date) + ", ساعت " +
                date.ToShortTimeString().ToUpper().Replace("AM", "ق.ظ").Replace("PM", "ب.ظ");
    }

    public static string GetTotalStringDate(DateTime date)
    {
        return GetPersianDayName(date) + ", " +
                GetPersianDay(date) + " " +
                GetPersianMonthName(date) + " " +
                GetPersianYear(date);
    }

    public static bool DateChanged(DateTime date1, DateTime date2)
    {
        return date1.Date != date2.Date;
    }

    public static DateTime ConvertFromUnixTimestamp(long timestamp)
    {
        return origin.AddSeconds(timestamp).ToLocalTime();
    }

    public static long ConvertToUnixTimestamp(DateTime date)
    {
        if (date != origin) date = date.ToUniversalTime();
        TimeSpan diff = date - origin;
        return (long)diff.TotalSeconds;
    }
}