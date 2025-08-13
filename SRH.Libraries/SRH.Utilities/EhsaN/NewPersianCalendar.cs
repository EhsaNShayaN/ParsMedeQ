using System.Runtime.InteropServices;

namespace SRH.Utilities.EhsaN;

public sealed class NewPersianCalendar : System.Globalization.PersianCalendar
{
    private int _year;
    private int _month;
    private int _day;
    private int _hour;
    private int _minute;
    private int _second;
    private int _milliseconds;
    private string _now = MiladiToPersian(DateTime.Now);
    private string _persianDate;
    private DateTime _gregorianDateTime;
    private string _hijriDate;




    private static string[] days = new string[] { 
        "اول", "دوم", "سوم", "چهارم", "پنجم", "ششم", "هفتم", "هشتم", "نهم", "دهم", "يازدهم", "دوازدهم", "سيزدهم", "چهاردهم", "پانزدهم", "شانزدهم", 
        "هفدهم", "هجدهم", "نوزدهم", "بيست", "بيست و يکم", "بيست و دوم", "بيست و سوم", "بيست و چهارم", "بيست و پنجم", "بيست و ششم", "بيست و هفتم", "بيست و هشتم", "بيست و نهم", "سي", "سي و يکم"
     };
    private static string[] months = new string[] { "فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دي", "بهمن", "اسفند" };
    private static string[] moonMonths = new string[] { "محرم", "صفر", "ربيع الاول", "ربيع الثاني", "جمادي الاولي", "جمادي الثانيه", "رجب", "شعبان", "رمضان", "شوال", "ذو القعده", "ذو الحجه" };
    private static string[] moonWeekDays = new string[] { "اِسَّبِت", "اِلأَحَّد", "اِلأِثنين", "اِثَّلاثا", "اِلأَربِعا", "اِلخَميس", "اِجُّمعَة" };
    private static string[] weekDays = new string[] { "شنبه", "يکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه" };


    [NonSerialized]
    public static NewPersianCalendar MaxValue = new NewPersianCalendar(DateTime.MaxValue);
    [NonSerialized]
    public static NewPersianCalendar MinValue = new NewPersianCalendar(1, 1, 1, 12, 0, 0, 0);

    #region Properties

    public static string[] DayNames
    {
        get
        {
            return new string[] { "شنبه", "يکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه" };
        }
    }
    public static DayOfWeek FirstDayOfWeek
    {
        get
        {
            return System.DayOfWeek.Saturday;
        }
    }

    public int Year
    {
        get { return _year; }
        set { _year = value; }
    }

    public int Month
    {
        get { return _month; }
        set { _month = value; }
    }

    public int Day
    {
        get { return _day; }
        set { _day = value; }
    }

    public int Hour
    {
        get { return _hour; }
        set { _hour = value; }
    }

    public int Minute
    {
        get { return _minute; }
        set { _minute = value; }
    }

    public int Second
    {
        get { return _second; }
        set { _second = value; }
    }

    public int Milliseconds
    {
        get { return _milliseconds; }
        set { _milliseconds = value; }
    }

    public static string Now
    {
        get
        {
            return MiladiToPersian(DateTime.Now);
            //return _now; 
        }
        //set { _now = value; }
    }

    public static string NowWithTime
    {
        get
        {
            return Now + " " + DateTime.Now.ToString("HH:mm:ss:ff");
        }
    }

    public DateTime GregorianDateTime
    {
        get { return _gregorianDateTime; }
        set { _gregorianDateTime = value; }
    }

    public string PersianDate
    {
        get { return _persianDate; }
        set { _persianDate = value; }
    }



    public string HijriDate
    {
        get { return _hijriDate; }
        set { _hijriDate = value; }
    }
    #endregion

    #region Cunstractor

    public NewPersianCalendar()
    {
        this._year = GetPersianYear(_now);
        this._month = GetPersianMonth(_now);
        this._day = GetPersianDay(_now);
        this._hour = DateTime.Now.Hour;
        this._minute = DateTime.Now.Minute;
        this._second = DateTime.Now.Second;
        this._milliseconds = DateTime.Now.Millisecond;
        _persianDate = _now;
        _gregorianDateTime = DateTime.Now;
    }

    public NewPersianCalendar(DateTime mDate)
    {
        if (mDate.Equals(MinValue))
        {
            mDate = MinValue.GregorianDateTime;
        }
        this._year = int.Parse(GetPersianYear(mDate));
        this._month = int.Parse(GetPersianMonth(mDate));
        this._day = int.Parse(GetPersianDay(mDate));
        this._hour = int.Parse(GetPersianHour(mDate));
        this._minute = int.Parse(GetPersianMinute(mDate));
        this._second = int.Parse(GetPersianSecond(mDate));
        this._milliseconds = int.Parse(GetPersianMilliseconds(mDate));
        _persianDate = MiladiToPersian(mDate);
        _gregorianDateTime = mDate;
    }

    public NewPersianCalendar(int year, int month, int day)
    {
        this.CheckYear(year);
        this.CheckMonth(month);
        this.CheckDay(year, month, day);
        this._year = year;
        this._month = month;
        this._day = day;
        this._hour = DateTime.Now.Hour;
        this._minute = DateTime.Now.Minute;
        this._second = DateTime.Now.Second;
        this._milliseconds = DateTime.Now.Millisecond;
        System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
        this._persianDate = MiladiToPersian(DateTime.Parse(string.Format("{0}/{1}/{2}", year.ToString(), month.ToString(), day.ToString())));
        _gregorianDateTime = ToDateTime(year, month, day, 0, 0, 0);
    }

    public NewPersianCalendar(int year, int month, int day, int hour, int minute, int second, int milliseconds)
    {
        this.CheckYear(year);
        this.CheckMonth(month);
        this.CheckDay(year, month, day);
        this.CheckHour(hour);
        this.CheckMinute(minute);
        this.CheckSecond(second);
        this.CheckMillisecond(milliseconds);
        this._year = year;
        this._month = month;
        this._day = day;
        this._hour = hour;
        this._minute = minute;
        this._second = second;
        this._milliseconds = milliseconds;
        this._persianDate = MiladiToPersian(DateTime.Parse(string.Format("{0}/{1}/{2} {3}:{4}:{5}", year.ToString(), month.ToString(), day.ToString()
            , hour.ToString(), minute.ToString(), second.ToString(), milliseconds.ToString())));
        this._gregorianDateTime = ToDateTime(year, month, day, hour, minute, second, milliseconds);
    }

    public NewPersianCalendar(int year, int month, int day, int hour, int minute)
        : this(year, month, day, hour, minute, 0)
    {
    }

    public NewPersianCalendar(int year, int month, int day, int hour, int minute, int second)
        : this(year, month, day, hour, minute, second, 0)
    {
    }

    #endregion

    #region PersianCalendar
    public static string GetNameOfDay(DateTime time, out int nIndex)
    {

        DayOfWeek nDay = time.DayOfWeek;
        string strDay = string.Empty;
        nIndex = 0;
        switch (nDay)
        {
            case System.DayOfWeek.Sunday:
                strDay = "يکشنبه";
                nIndex = 1;
                break;

            case System.DayOfWeek.Monday:
                strDay = "دوشنبه";
                nIndex = 2;
                break;

            case System.DayOfWeek.Tuesday:
                strDay = "سه شنبه";
                nIndex = 3;
                break;

            case System.DayOfWeek.Wednesday:
                strDay = "چهارشنبه";
                nIndex = 4;
                break;

            case System.DayOfWeek.Thursday:
                strDay = "پنج شنبه";
                nIndex = 5;
                break;

            case System.DayOfWeek.Friday:
                strDay = "جمعه";
                nIndex = 6;
                break;

            case System.DayOfWeek.Saturday:
                strDay = "شنبه";
                nIndex = 0;
                break;
        }
        return strDay;
    }

    public static string GetNameOfMonth(DateTime time)
    {
        int monthIndex = new System.Globalization.PersianCalendar().GetMonth(time);
        switch (monthIndex)
        {
            case 1:
                return "فروردين";

            case 2:
                return "ارديبهشت";

            case 3:
                return "خرداد";

            case 4:
                return "تير";

            case 5:
                return "مرداد";

            case 6:
                return "شهريور";

            case 7:
                return "مهر";

            case 8:
                return "آبان";

            case 9:
                return "آذر";

            case 10:
                return "دي";

            case 11:
                return "بهمن";

            case 12:
                return "اسفند";
        }
        return "";
    }
    public enum dayNames
    {
        Friday = 5,
        Monday = 1,
        None = -1,
        Saturday = 6,
        Sunday = 0,
        Thursday = 4,
        Tuesday = 2,
        Wednesday = 3
    }

    private static int[] g_days_in_month = new int[] { 0x1f, 0x1c, 0x1f, 30, 0x1f, 30, 0x1f, 0x1f, 30, 0x1f, 30, 0x1f };
    private static int[] j_days_in_month = new int[] { 0x1f, 0x1f, 0x1f, 0x1f, 0x1f, 0x1f, 30, 30, 30, 30, 30, 0x1d };

    public static int Compare(string pDate1, string pDate2)
    {
        DateTime date1 = PersianToMiladiDate(pDate1);
        DateTime date2 = PersianToMiladiDate(pDate2);
        return DateTime.Compare(date1, date2);
    }

    public enum DiffType
    {
        Years,
        Days,
        Hours,
        Minutes,
        Seconds,
        Millisecond
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pDate1">Persian Date value</param>
    /// <param name="pDate2">Persian Date value</param>
    /// <param name="difftype">Format of return value: Days, Hours, Minutes, Seconds or Millisecond</param>
    /// <returns>a double value</returns>
    public static double DateDiff(string pDate1, string pDate2, DiffType difftype)
    {

        DateTime date1 = PersianToMiladiDate(pDate1);
        DateTime date2 = PersianToMiladiDate(pDate2);
        TimeSpan diff = date1 - date2;

        switch (difftype)
        {

            case DiffType.Days:
                return diff.TotalDays;
            case DiffType.Hours:
                return diff.TotalHours;
            case DiffType.Minutes:
                return diff.TotalMinutes;
            case DiffType.Seconds:
                return diff.TotalSeconds;
            case DiffType.Millisecond:
                return diff.TotalMilliseconds;
            case DiffType.Years:
                return diff.TotalDays / 365;
            default:
                return diff.TotalDays;


        }
    }

    public static double DateDiff(DateTime pDate1, DateTime pDate2)
    {
        return DateDiff(pDate1, pDate2, DiffType.Days);
    }

    public static double DateDiff(DateTime pDate1, DateTime pDate2, DiffType difftype)
    {
        return DateDiff(MiladiToShortPersian(pDate1), MiladiToShortPersian(pDate2), difftype);
    }

    /// <summary>
    /// Get difference between two Persian Date
    /// </summary>
    /// <param name="pDate1">Persian Date value</param>
    /// <param name="pDate2">Persian Date value</param>
    /// <returns>a double value in Days Format</returns>
    public static double DateDiff(string pDate1, string pDate2)
    {
        return DateDiff(pDate1, pDate2, DiffType.Days);
    }

    public static void BetweenDate(string fromDate, string toDate, out int year, out int month, out int day)
    {
        if (IsDate(fromDate) || IsDate(toDate))
        {
            day = 0;
            month = 0;
            year = 0;
        }
        else
        {
            if (Compare(fromDate, toDate) == 1)
            {
                string TempDate = fromDate;
                fromDate = toDate;
                toDate = TempDate;
            }
            int Azyear = Convert.ToInt32(fromDate.Substring(0, 4).ToString());
            int Azmonth = Convert.ToInt32(fromDate.Substring(5, 2).ToString());
            int Azday = Convert.ToInt32(fromDate.Substring(8, 2).ToString());
            int Tayear = Convert.ToInt32(toDate.Substring(0, 4).ToString());
            int Tamonth = Convert.ToInt32(toDate.Substring(5, 2).ToString());
            int Taday = Convert.ToInt32(toDate.Substring(8, 2).ToString());
            if (Taday >= Azday)
            {
                day = Taday - Azday;
            }
            else
            {
                if ((Tamonth <= 6) && (Tamonth > 1))
                {
                    Taday += 0x1f;
                }
                else if (((Tamonth < 12) && (Tamonth > 6)) || (Tamonth == 1))
                {
                    Taday += 30;
                }
                else
                {
                    Taday += 0x1d;
                }
                Tamonth--;
                day = Taday - Azday;
            }
            if (Tamonth >= Azmonth)
            {
                month = Tamonth - Azmonth;
            }
            else
            {
                Tamonth += 12;
                Tayear--;
                month = Tamonth - Azmonth;
            }
            year = Tayear - Azyear;
        }
    }


    private void CheckYear(int YearNo)
    {
        if ((YearNo < 1) || (YearNo > 0x270f))
        {
            throw new PersianDateTimeException();
        }
    }

    private void CheckMonth(int MonthNo)
    {
        if ((MonthNo > 12) || (MonthNo < 1))
        {
            throw new PersianDateTimeException();
        }
    }

    private void CheckDay(int YearNo, int MonthNo, int DayNo)
    {
        if ((MonthNo < 6) && (DayNo > 0x1f))
        {
            throw new PersianDateTimeException();
        }
        if ((MonthNo > 6) && (DayNo > 30))
        {
            throw new PersianDateTimeException();
        }
        if (((MonthNo == 12) && (DayNo > 0x1d)) && (!this.IsLeapDay(YearNo, MonthNo, DayNo) || (DayNo > 30)))
        {
            throw new PersianDateTimeException();
        }
    }

    private void CheckHour(int HourNo)
    {
        if ((HourNo > 0x18) || (HourNo < 0))
        {
            throw new PersianDateTimeException();
        }
    }

    private void CheckMinute(int MinuteNo)
    {
        if ((MinuteNo > 60) || (MinuteNo < 0))
        {
            throw new PersianDateTimeException();
        }
    }

    private void CheckSecond(int SecondNo)
    {
        if ((SecondNo > 60) || (SecondNo < 0))
        {
            throw new PersianDateTimeException();
        }
    }

    private void CheckMillisecond(int MillisecondNo)
    {
        if ((MillisecondNo < 0) || (MillisecondNo > 0x3e8))
        {
            throw new PersianDateTimeException();
        }
    }


    public static bool IsValid(string persianDate)
    {
        if (persianDate == "9999/99/99")
            return true;
        try
        {
            PersianToMiladiDate(persianDate);
            return true;
        }
        catch (Exception)
        {

            return false;
        }
    }


    public static bool MiladiIsValid(string miladiDate)
    {
        if (miladiDate == "9999/99/99")
            return true;
        try
        {
            MiladiToPersianReverseFormat(Convert.ToDateTime(miladiDate));
            return true;
        }
        catch (Exception)
        {

            return false;
        }
    }
    /// <summary>
    /// Returns a string Persian date that is offset the specified number of days
    /// from the specified string Persian date object.
    /// </summary>
    /// <param name="pDate">Persian string date format value</param>
    /// <param name="days">integer value</param>
    /// <returns>string Persian date format</returns>
    public static string AddDays(string pDate, int days)
    {
        System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();
        return MiladiToPersian(persianCalendar.AddDays(PersianToMiladiDate(pDate), days));
    }


    /// <summary>
    ///  Returns a string Persian date that is offset the specified number of months
    ///  from the specified string Persian date.
    /// </summary>
    /// <param name="pDate">The Persian Date to add months to</param>
    /// <param name="months">The positive or negative number of months to add</param>
    /// <returns>A Persian Date that represents the date yielded by adding the number
    ///     of months specified by the months parameter to the date specified by the
    ///     pDate parameter.</returns>
    public static string AddMonths(string pDate, int months)
    {
        System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();
        return MiladiToPersian(persianCalendar.AddMonths(PersianToMiladiDate(pDate), months));
    }

    /// <summary>
    /// Returns a Persian Date that is offset the specified number of years
    ///     from the specified Persian Date.
    /// </summary>
    /// <param name="pDate">The Persian Date to add years to.</param>
    /// <param name="years">The positive or negative number of years to add.</param>
    /// <returns> The Persian Date that results from adding the specified number
    ///     of years to the specified Persian Date.</returns>
    public static string AddYears(string pDate, int years)
    {
        System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();
        return MiladiToPersian(persianCalendar.AddYears(PersianToMiladiDate(pDate), years));
    }

    private static dayNames DayOfWeek(DateTime mDate)
    {
        int num6 = 0;
        if (IsDate(mDate.ToString()))
        {
            return dayNames.None;
        }
        int num2 = mDate.Year / 100;
        int num8 = mDate.Year % 100;
        int month = mDate.Month;
        int day = mDate.Day;
        int num = Math.Abs((int)((num2 % 4) - 3)) * 2;
        num = num % 7;
        int num9 = ((num8 / 12) + (num8 % 12)) + ((num8 % 12) / 4);
        num9 = num9 % 7;
        switch (month)
        {
            case 1:
                num6 = 0;
                break;

            case 2:
                num6 = 3;
                break;

            case 3:
                num6 = 3;
                break;

            case 4:
                num6 = 6;
                break;

            case 5:
                num6 = 1;
                break;

            case 6:
                num6 = 4;
                break;

            case 7:
                num6 = 6;
                break;

            case 8:
                num6 = 2;
                break;

            case 9:
                num6 = 5;
                break;

            case 10:
                num6 = 0;
                break;

            case 11:
                num6 = 3;
                break;

            case 12:
                num6 = 12;
                break;
        }
        int num4 = day % 7;
        int num7 = (((num + num9) + num6) + num4) % 7;
        if (IsMiladiLeapYear((num2 * 100) + num8) & ((month == 1) | (month == 2)))
        {
            num7 = (num7 == 0) ? 7 : num7;
            num7--;
        }
        return (dayNames)num7;
    }

    /// <summary>
    /// Show the name of week day
    /// </summary>
    /// <param name="pDate">Gregorian DateTime value</param>
    /// <returns>string value</returns>
    public static string DayOfWeekMiladi(DateTime pDate)
    {
        switch (DayOfWeek(pDate))
        {
            case dayNames.Sunday:
                return "Sunday";

            case dayNames.Monday:
                return "Monday";

            case dayNames.Tuesday:
                return "Tuesday";

            case dayNames.Wednesday:
                return "Wednesday";

            case dayNames.Thursday:
                return "Thursday";

            case dayNames.Friday:
                return "Friday";

            case dayNames.Saturday:
                return "Saturday";
        }
        return "";
    }

    /// <summary>
    ///  نمایش نام فارسی روز هفته با دریافت تاریخ شمسی
    /// </summary>
    /// <param name="pDate">Persian Date, string value</param>
    /// <returns>string value</returns>
    public static string DayOfWeekPersian(string pDate)
    {
        DateTime miladiDate = PersianToMiladiDate(pDate);
        switch (DayOfWeek(miladiDate))
        {
            case dayNames.Sunday:
                return "يکشنبه";

            case dayNames.Monday:
                return "دوشنبه";

            case dayNames.Tuesday:
                return "سه شنبه";

            case dayNames.Wednesday:
                return "چهارشنبه";

            case dayNames.Thursday:
                return "پنج شنبه";

            case dayNames.Friday:
                return "جمعه";

            case dayNames.Saturday:
                return "شنبه";
        }
        return "";
    }

    /// <summary>
    /// Get Gregorian day value
    /// </summary>
    /// <param name="mNow">DateTime value</param>
    /// <returns>integer value</returns>
    public static int GetMildayDay(DateTime mNow)
    {
        return mNow.Day;
    }

    /// <summary>
    /// Get Gregorian month value
    /// </summary>
    /// <param name="mNow">DateTime value</param>
    /// <returns>integer value</returns>
    public static int GetMiladiMonth(DateTime mNow)
    {
        return mNow.Month;
    }

    /// <summary>
    /// Get Gregorian Year
    /// </summary>
    /// <param name="mNow">Gregorian Now DateTime</param>
    /// <returns>year integer value</returns>
    public static int GetMiladiYear(DateTime mNow)
    {
        return mNow.Year;
    }

    /// <summary>
    /// Get number of days in input Persian month
    /// </summary>
    /// <param name="monthIndex">a integer value between 0-11
    /// 0:January, 1:February, 2:March
    /// 3:April, 4:May, 5:June
    /// 6:July, 7:August, 8:September
    /// 9:October, 10:November, 11:December</param>
    /// <param name="persianYear">Gregorian integer year value</param>
    /// <returns></returns>
    public static int GetMiladiMonthDays(byte monthIndex, [Optional, DefaultParameterValue(-1)] int miladiYear)
    {
        int num2 = g_days_in_month[monthIndex];
        if (((monthIndex == 1) & (miladiYear != -1)) && IsMiladiLeapYear(miladiYear))
        {
            num2++;
        }
        return num2;
    }


    /// <summary>
    /// Get number of days in input month number
    /// </summary>
    /// <param name="monthIndex">a integer value between 0-11
    /// 0:farvardin, 1:ordibehesht, 2:khordad
    /// 3:tir, 4:mordad, 5:shahrivar
    /// 6:mehr, 7:aban, 8:azar
    /// 9:dey, 10:bahman, 11:esfand</param>
    /// <param name="persianYear">Persian integer year value</param>
    /// <returns></returns>
    public static int GetPersianMonthDays(byte monthIndex, [Optional, DefaultParameterValue(-1)] int persianYear)
    {
        int num2 = j_days_in_month[monthIndex];
        if (((monthIndex == 11) & (persianYear != -1)) && IsPersianLeapYear(persianYear))
        {
            num2++;
        }
        return num2;
    }

    /// <summary>
    /// Indicate that input integer Gregorian year parameter is a Leap year or no
    /// </summary>
    /// <param name="myear">Gregorian year integer value</param>
    /// <returns>a Boolean value</returns>
    public static bool IsMiladiLeapYear(int myear)
    {
        if ((myear % 4) != 0)
        {
            return false;
        }
        if (((myear % 100) == 0) & ((myear % 400) != 0))
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Indicate that input integer Persian year parameter is a Leap year or no
    /// </summary>
    /// <param name="myear">Persian year integer value</param>
    /// <returns>a Boolean value</returns>
    public static bool IsPersianLeapYear(int myear)
    {
        int num = myear;
        bool flag2 = false;
        switch ((myear % 0x21))
        {
            case 1:
            case 5:
            case 9:
            case 13:
            case 0x11:
            case 0x16:
            case 0x1a:
            case 30:
                return true;

            case 2:
            case 3:
            case 4:
            case 6:
            case 7:
            case 8:
            case 10:
            case 11:
            case 12:
            case 14:
            case 15:
            case 0x10:
            case 0x12:
            case 0x13:
            case 20:
            case 0x15:
            case 0x17:
            case 0x18:
            case 0x19:
            case 0x1b:
            case 0x1c:
            case 0x1d:
                return flag2;
        }
        return flag2;
    }

    /// <summary>
    /// Get year int value of input DateTime parameter
    /// </summary>
    /// <param name="mDate">Gregorian DateTime value</param>
    /// <returns>string value of Persian year</returns>
    public static string GetPersianYear(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.PersianCalendar persianDate = new System.Globalization.PersianCalendar();
        string y = persianDate.GetYear(mDate).ToString();
        return y;
    }


    /// <summary>
    /// Convert Gregorian DateTime value to short format of Persian date 
    /// </summary>
    /// <param name="mDate">Gregorian DateTime value</param>
    /// <returns>Short format of Persian date</returns>
    public static string GetPersianShortYear(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.PersianCalendar persianDate = new System.Globalization.PersianCalendar();
        string y = persianDate.GetYear(mDate).ToString();
        return y.Substring(2, 2);
    }

    /// <summary>
    /// Get Persian month value of Gregorian DateTime input parameter
    /// </summary>
    /// <param name="mDate">Gregorian DateTime value</param>
    /// <returns>string value of Persian month of year</returns>
    public static string GetPersianMonth(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.PersianCalendar persianDate = new System.Globalization.PersianCalendar();
        string m = persianDate.GetMonth(mDate).ToString();
        if (m.Length < 2)
            m = "0" + m;
        return m;
    }



    /// <summary>
    /// Get Persian day of month value
    /// </summary>
    /// <param name="mDate">Gregorian DateTime value</param>
    /// <returns>string value of Persian day of month</returns>
    public static string GetPersianDay(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.PersianCalendar persianDate = new System.Globalization.PersianCalendar();
        string d = persianDate.GetDayOfMonth(mDate).ToString();
        if (d.Length < 2)
            d = "0" + d.ToString();
        return d;
    }


    public static string GetPersianHour(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.PersianCalendar persianDate = new System.Globalization.PersianCalendar();
        string h = persianDate.GetHour(mDate).ToString();
        if (h.Length < 2)
            h = "0" + h.ToString();
        return h;
    }



    public static string GetPersianMinute(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.PersianCalendar persianDate = new System.Globalization.PersianCalendar();
        string m = persianDate.GetMinute(mDate).ToString();
        if (m.Length < 2)
            m = "0" + m.ToString();
        return m;
    }




    public static string GetPersianSecond(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.PersianCalendar persianDate = new System.Globalization.PersianCalendar();
        string s = persianDate.GetSecond(mDate).ToString();
        if (s.Length < 2)
            s = "0" + s.ToString();
        return s;
    }


    public static string GetPersianMilliseconds(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.PersianCalendar persianDate = new System.Globalization.PersianCalendar();
        string ms = persianDate.GetMilliseconds(mDate).ToString();
        if (ms.Length < 2)
            ms = "0" + ms.ToString();
        return ms;
    }



    public static int GetPersianYear(string pDate)
    {
        string[] date = pDate.Split(new char[] { '/' });
        return int.Parse(date[0]);
    }

    public static int GetPersianMonth(string pDate)
    {
        string[] date = pDate.Split(new char[] { '/' });
        return int.Parse(date[1]);
    }

    public static int GetPersianDay(string pDate)
    {
        string[] date = pDate.Split(new char[] { '/' });
        return int.Parse(date[2]);
    }




    #endregion

    #region HijriCalendar

    public static int GetHijriDay(string mHijriDate)
    {
        string[] date = mHijriDate.Split(new char[] { '/' });
        return int.Parse(date[2]);
    }

    public static int GetHijriMonth(string mHijriDate)
    {
        string[] date = mHijriDate.Split(new char[] { '/' });
        return int.Parse(date[1]);
    }

    public static int GetHijriYear(string mHijriDate)
    {
        string[] date = mHijriDate.Split(new char[] { '/' });
        return int.Parse(date[0]);
    }

    public static int GetHijriYear(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.HijriCalendar hijriCalendar = new System.Globalization.HijriCalendar();
        return hijriCalendar.GetYear(mDate);
    }

    /// <summary>
    /// Convert Gregorian DateTime value to short format of Hijri date 
    /// </summary>
    /// <param name="mDate">Gregorian DateTime value</param>
    /// <returns>Short format of Hijri date</returns>
    public static string GetHijriShortYear(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.HijriCalendar hijriCalendar = new System.Globalization.HijriCalendar();
        string y = hijriCalendar.GetYear(mDate).ToString();
        return y.Substring(2, 2);
    }

    /// <summary>
    /// Get Hijri month value of Gregorian DateTime input parameter
    /// </summary>
    /// <param name="mDate">Gregorian DateTime value</param>
    /// <returns>string value of Hijri month of year</returns>
    public static int GetHijriMonth(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.HijriCalendar hijriCalendar = new System.Globalization.HijriCalendar();
        return hijriCalendar.GetMonth(mDate);
    }



    /// <summary>
    /// Get Hijri day of month value
    /// </summary>
    /// <param name="mDate">Gregorian DateTime value</param>
    /// <returns>string value of Hijri day of month</returns>
    public static int GetHijriDay(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.HijriCalendar hijriCalendar = new System.Globalization.HijriCalendar();
        return hijriCalendar.GetDayOfMonth(mDate);
    }


    public static int GetHijriHour(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.HijriCalendar hijriCalendar = new System.Globalization.HijriCalendar();
        return hijriCalendar.GetHour(mDate);
    }



    public static int GetHijriMinute(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.HijriCalendar hijriCalendar = new System.Globalization.HijriCalendar();
        return hijriCalendar.GetMinute(mDate);
    }




    public static int GetHijriSecond(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.HijriCalendar hijriCalendar = new System.Globalization.HijriCalendar();
        return hijriCalendar.GetSecond(mDate);
    }


    public static double GetHijriMilliseconds(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.HijriCalendar hijriCalendar = new System.Globalization.HijriCalendar();
        return hijriCalendar.GetMilliseconds(mDate);
    }

    public static int GetHijriDayOfWeek(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        System.Globalization.HijriCalendar hijriCalendar = new System.Globalization.HijriCalendar();
        return hijriCalendar.GetDayOfMonth(mDate);
    }

    #endregion

    #region GregorianToPersian

    /// <summary>
    /// Convert Gregorian Date To Short Persian Date like this yyyy/mm/dd
    /// </summary>
    /// <param name="mDate">DateTime Value</param>
    /// <returns>Persian Date String Value</returns>
    public static string MiladiToPersian(DateTime mDate)
    {
        try
        {
            if (mDate.Equals(MinValue))
                mDate = MinValue.GregorianDateTime;
            return String.Format("{0}/{1}/{2}", GetPersianYear(mDate), GetPersianMonth(mDate), GetPersianDay(mDate));
        }
        catch
        {
            throw new PersianDateTimeException();
        }

    }

    public static string MiladiToPersianReverseFormat(DateTime mDate)
    {
        try
        {
            if (mDate.Equals(MinValue))
                mDate = MinValue.GregorianDateTime;
            return String.Format("{0}/{1}/{2}", GetPersianDay(mDate), GetPersianMonth(mDate), GetPersianYear(mDate));
        }
        catch
        {
            throw new PersianDateTimeException();
        }

    }

    /// <summary>
    /// Convert Gregorian Date To Short Persian Date like this yy/mm/dd
    /// </summary>
    /// <param name="mDate">DateTime Value</param>
    /// <returns>Persian Date String Value</returns>
    public static string MiladiToShortPersian(DateTime mDate)
    {
        if (!IsDate(mDate.ToString())) throw new PersianDateTimeException();

        string shortDate = MiladiToPersian(mDate);
        return shortDate.Substring(2);
    }

    #endregion

    #region PersianToGregorian
    /// <summary>
    /// Convert Persian string format DateTime To Gregorian DateTime
    /// </summary>
    /// <param name="pDate">Input persian Date format must be like this: "yyyy/MM/dd hh:mm" or "yy/MM/dd hh:mm"</param>
    /// <returns>return a DateTime</returns>
    public static DateTime PersianToMiladiDateTime(string pDate)
    {
        string[] textArray = pDate.Split(new char[] { '/', ' ', ':' });
        int year = 1300;
        if (textArray[0].Length == 2)
            year = Convert.ToInt32("13" + textArray[0]);
        else if (textArray[0].Length == 4)
            year = Convert.ToInt32(textArray[0]);
        else return Convert.ToDateTime(null);
        int month = Convert.ToInt32(textArray[1]);
        int day = Convert.ToInt32(textArray[2]);
        int hour = 0;
        int minute = 0;
        if (textArray.Length > 3)
        {
            hour = Convert.ToInt32(textArray[3]);
            minute = Convert.ToInt32(textArray[4]);
        }
        return PersianToMiladiDateTime(year, month, day, hour, minute, 0);
    }

    /// <summary>
    /// Convert Persian String Full Date Or Short Date To Gregorian Date
    /// </summary>
    /// <param name="pDate">Input persian Date format must be like this: "yyyy/mm/dd" or "yy/mm/dd"</param>
    /// <returns>return a Gregorian Date</returns>
    public static DateTime PersianToMiladiDate(string pDate)
    {
        if (pDate.Length < 4)
            return new DateTime();
        string[] textArray = pDate.Split(new char[] { '/' });
        int year = 1300;
        if (textArray[0].Length == 2)
            year = Convert.ToInt32("13" + textArray[0]);
        else if (textArray[0].Length == 4)
            year = Convert.ToInt32(textArray[0]);
        else return Convert.ToDateTime(null);
        int month = Convert.ToInt32(textArray[1]);
        int day = Convert.ToInt32(textArray[2]);
        return PersianToMiladiDate(year, month, day);
    }

    /// <summary>
    /// Convert Persian Date To Gregorian Date
    /// </summary>
    /// <param name="pYear">Persian Year</param>
    /// <param name="pMonth">Persian Month</param>
    /// <param name="pDay">Persian Day</param>
    /// <returns>Gregorian Date</returns>
    public static DateTime PersianToMiladiDate(int pYear, int pMonth, int pDay)
    {
        try
        {
            System.Globalization.PersianCalendar persianDate = new System.Globalization.PersianCalendar();
            return persianDate.ToDateTime(pYear, pMonth, pDay, 0, 0, 0, 0);
        }
        catch (System.ArgumentOutOfRangeException)
        {
            throw new System.ArgumentException("The resulting DateTime is outside the supported range.", "months");
        }

    }

    /// <summary>
    /// Convert Persian DateTime To Gregorian DateTime 
    /// </summary>
    /// <param name="pYear">Persian Year</param>
    /// <param name="pMonth">Persian Month</param>
    /// <param name="pDay">Persian Day</param>
    /// <param name="pHour">Hour</param>
    /// <param name="pMinute">Minute</param>
    /// <param name="pSecond">Second</param>
    /// <returns>Gregorian DateTime Value</returns>
    public static DateTime PersianToMiladiDateTime(int pYear, int pMonth, int pDay, int pHour, int pMinute, int pSecond)
    {
        System.Globalization.PersianCalendar persianDate = new System.Globalization.PersianCalendar();
        return persianDate.ToDateTime(pYear, pMonth, pDay, pHour, pMinute, pSecond, 0);
    }


    /// <summary>
    /// Convert Persian DateTime To Gregorian DateTime 
    /// </summary>
    /// <param name="pYear">Persian Year int value</param>
    /// <param name="pMonth">Persian Month int value</param>
    /// <param name="pDay">Persian Day int value</param>
    /// <param name="pHour">Hour int value</param>
    /// <param name="pMinute">Minute int value</param>
    /// <param name="pSecond">Second int value</param>
    /// <param name="pMillisecond">Millisecond int value</param>
    /// <returns>Gregorian DateTime Value</returns>
    public static DateTime PersianToMiladiDateTime(int pYear, int pMonth, int pDay, int pHour, int pMinute, int pSecond, int pMillisecond)
    {
        System.Globalization.PersianCalendar persianDate = new System.Globalization.PersianCalendar();
        return persianDate.ToDateTime(pYear, pMonth, pDay, pHour, pMinute, pSecond, pMillisecond);
    }

    /// <summary>
    /// Convert Persian Date To gregorian Date
    /// </summary>
    /// <param name="stringDate">Persian Date</param>
    /// <returns>Gregorian DateTime Value</returns>
    public static DateTime StringToDate(string stringDate)
    {
        return PersianToMiladiDate(stringDate);
    }

    public static DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second)
    {
        System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();
        return persianCalendar.ToDateTime(year, month, day, hour, month, second, 0);
    }
    #endregion

    /// <summary>
    /// مقایسه برای تاریخ فارسی
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static bool IsGreaterThanFromNow(string dateTime)
    {
        if (string.Compare(Now, dateTime) == -1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// مقایسه برای تاریخ فارسی
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static bool IsGreaterOrEqualThanFromNow(string dateTime)
    {
        if (string.Compare(Now, dateTime) == 0 || string.Compare(Now, dateTime) == -1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// مقایسه برای تاریخ فارسی
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static bool IsLessOrEqualThanFromNow(string dateTime)
    {
        if (string.Compare(Now, dateTime) == 0 || string.Compare(Now, dateTime) == +1)
            return true;
        else
            return false;
    }

    /// <summary>
    /// مقایسه برای تاریخ فارسی
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static bool IsLessThanFromNow(string dateTime)
    {
        if (string.Compare(Now, dateTime) == +1)
            return true;
        else
            return false;
    }

    private static bool IsNumeric(string anyString)
    {
        if (anyString == null)
        {
            anyString = "";
        }
        if (anyString.Length > 0)
        {
            double dummyOut = new double();
            System.Globalization.CultureInfo cultureInfo =
                new System.Globalization.CultureInfo("en-US", true);

            return Double.TryParse(anyString, System.Globalization.NumberStyles.Any,
                cultureInfo.NumberFormat, out dummyOut);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// indicate that input parameter is a valid date or not
    /// </summary>
    /// <param name="anyString">gregorian date string value</param>
    /// <param name="resultDate">Convert valid string to DateTime value</param>
    /// <returns>a Boolean value</returns>
    private static bool IsDate(string anyString, out DateTime resultDate)
    {
        bool isDate = true;

        if (anyString == null)
        {
            anyString = "";
        }
        try
        {
            resultDate = DateTime.Parse(anyString);
        }
        catch
        {
            resultDate = DateTime.MinValue;
            isDate = false;
        }

        return isDate;
    }

    /// <summary>
    /// indicate that input parameter is a valid date or not
    /// </summary>
    /// <param name="anyString">gregorian date string value</param>
    /// <returns>a Boolean value</returns>
    private static bool IsDate(string anyString)
    {
        if (anyString == null)
        {
            anyString = "";
        }
        if (anyString.Length > 0)
        {
            DateTime dummyDate = Convert.ToDateTime(null);
            try
            {
                dummyDate = DateTime.Parse(anyString);
            }
            catch
            {
                return false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public static double Minus(string firstPersianDate, string secondPersianDate)
    {
        if (string.IsNullOrEmpty(firstPersianDate) || string.IsNullOrEmpty(secondPersianDate))
            return 0;
        return (PersianToMiladiDate(firstPersianDate) - PersianToMiladiDate(secondPersianDate)).TotalDays;
    }

    public static string TimeNow
    {
        get { return DateTime.Now.Hour.ToString("0#") + ":" + DateTime.Now.Minute.ToString("0#"); }
    }

    public static bool IsValisTime(string time)
    {
        if (time.Length != 5)
            return false;
        try
        {
            int hour = Convert.ToInt16(time.Split(':')[0].ToString());
            int minute = Convert.ToInt16(time.Split(':')[1].ToString());

            if (hour >= 24 || minute >= 60)
                return false;

        }
        catch (Exception)
        {

            return false;
        }

        return true;
    }
}
class PersianDateTimeException : Exception
{
    public PersianDateTimeException()
        : base("تاریخ نامعتبر است")
    {
    }
}