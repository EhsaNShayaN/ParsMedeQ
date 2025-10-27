using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace SRH.Utilities.EhsaN;

public static class ExtensionMethods
{
    public static string Encrypt2(this object obj)
    {
        if (obj == null) return string.Empty;
        return Encryption.Encrypt2(obj.ToString());
    }
    public static string Decrypt2(this object obj)
    {
        if (obj == null) return string.Empty;
        return Encryption.Decrypt2(obj.ToString());
    }
    public static int ToInt(this object obj, int defaultValue = -1)
    {
        if (obj != null)
        {
            var temp = obj.ToString();
            temp = temp.Replace(",", "").Replace("،", "");
            if (temp.IndexOf(".") > -1)
                temp = temp.Substring(0, temp.IndexOf("."));
            if (int.TryParse(temp, out int i)) return i;
        }
        return defaultValue;
    }
    public static long ToLong(this object obj, int defaultValue = -1)
    {
        if (obj != null)
        {
            var temp = obj.ToString();
            temp = temp.Replace(",", "").Replace("،", "");
            if (temp.IndexOf(".") > -1)
                temp = temp.Substring(0, temp.IndexOf("."));
            if (long.TryParse(temp, out long i)) return i;
        }
        return defaultValue;
    }
    public static long? ToNullLong(this object obj)
    {
        var result = obj.ToLong();
        if (result == -1) return null;
        return result;
    }
    public static Guid? ToGuid(this object obj)
    {
        if (obj != null)
        {
            var temp = obj.ToString();
            return new Guid(temp);
        }
        return null;
    }
    public static bool ToNumber(this object obj, bool defaultValue = false)
    {
        if (obj == null || string.IsNullOrEmpty(obj.ToString())) return defaultValue;
        foreach (var ch in obj.ToString())
        {
            if (!int.TryParse(ch.ToString(), out _)) return false;
        }
        return true;
    }
    public static DateTime? ToDateTime(this object obj)
    {
        if (obj == null) return null;
        var number = obj.ConvertToGeorgianNumber();
        if (string.IsNullOrEmpty(number)) return null;
        if (DateTime.TryParse(number, out DateTime i)) return i;
        return null;
    }
    public static string ToPersianDate(this DateTime date)
    {
        return PersianCalender.GetPersianDate(date);
    }
    public static string ToPersianDateTime(this DateTime date)
    {
        return PersianCalender.GetPersianDate(date) + " " + date.Hour + ":" + date.Minute;
    }
    public static string ToPersianDateTimeString(this DateTime date)
    {
        var dayName = PersianCalender.GetPersianDayName(date);
        var dayNumber = PersianCalender.GetPersianDay(date);
        var monthName = PersianCalender.GetPersianMonthName(date);
        var year = PersianCalender.GetPersianYear(date);
        return $"{dayName} {dayNumber} {monthName} {year} ساعت {date.Hour}:{date.Minute}";
    }
    public static string ToPersianDate(this DateTime? date)
    {
        return PersianCalender.GetPersianDate(date);
    }
    public static string AddPersianMonths(this DateTime date, int months)
    {
        return PersianCalender.AddPersianMonths(date, months).ToPersianDate();
    }
    private static DateTime ToGeorgianDate(this string date, DateTime? defaultDate = null)
    {
        try
        {
            date = date.Replace(".", "/").Replace("-", "/").Replace("_", "/").Replace("\\", "/");
            return PersianCalender.ConvertToGeorgianDateTime(date);
        }
        catch
        {
            if (!defaultDate.HasValue)
                throw;
        }
        return defaultDate.Value;
    }
    public static DateTime ToGeorgianDate(this string date)
    {
        if (!string.IsNullOrEmpty(date)) date = date.ConvertToGeorgianNumber();
        return date.ToGeorgianDate(null);
    }
    public static string ToCurrency(this object i, bool IsPersian = false)
    {
        return GetCurrencyFormat(i, IsPersian);
    }
    public static string GetCurrencyFormat(object objStr, bool IsPersian = true)
    {
        string Delimeter = IsPersian ? "،" : ",";
        bool IsNegative = false;
        if (objStr == null) return string.Empty;
        string baseStr = objStr.ToString();
        baseStr = baseStr.Replace(Delimeter, string.Empty);
        if (baseStr.IndexOf("-") == 0)
        {
            IsNegative = true;
            baseStr = baseStr.Remove(0, 1);
        }
        if (!string.IsNullOrEmpty(baseStr) && baseStr.Length > 3)
        {
            int idx = baseStr.Length - 3;
            while (idx >= 0)
            {
                baseStr = string.Format("{0}{1}{2}", baseStr.Substring(0, idx), Delimeter, baseStr.Substring(idx));
                idx -= 3;
            }
            baseStr = baseStr.Trim();
            if (baseStr.StartsWith(Delimeter)) baseStr = baseStr.Substring(1);
            if (baseStr.EndsWith(Delimeter)) baseStr = baseStr.Substring(0, baseStr.Length - 1);
        }
        if (IsNegative)
            baseStr = "-" + baseStr;
        return baseStr;
    }
    public static string ValidateMobile(this string mobile, string CountryCode, int NumbersCount = 8, bool OwnNumber = false, bool PreNumber = false)
    {
        var number = mobile;
        if (string.IsNullOrEmpty(number)) return string.Empty;
        number = number.Replace("+", "").Replace("-", "").Replace(" ", "");
        if (number.StartsWith("0"))
            number = number.Remove(0, 1);
        if (number.StartsWith("0"))
            number = number.Remove(0, 1);
        if (number.StartsWith("98"))
            number = number.Remove(0, 2);
        if (!PreNumber)
        {
            var regex = new Regex(@"^9[0-9]\d{" + NumbersCount + "}$");
            if (!regex.IsMatch(number)) return OwnNumber ? mobile : string.Empty;
        }
        number = CountryCode + number;
        return number;
    }
    public static string ToMobile(this object mobile)
    {
        var number = mobile.ConvertToGeorgianNumber();
        if (string.IsNullOrEmpty(number)) return null;
        number = number.Replace("+", "").Replace("-", "").Replace(" ", "");
        if (number.StartsWith("0"))
            number = number.Remove(0, 1);
        if (number.StartsWith("0"))
            number = number.Remove(0, 1);
        if (number.StartsWith("98"))
            number = number.Remove(0, 2);
        var regex = new Regex(@"^9[0-9]\d{8}$");
        if (!regex.IsMatch(number)) return null;
        return number;
    }
    public static bool ValidateMobile(this object mobile)
    {
        var number = mobile.ConvertToGeorgianNumber();
        if (string.IsNullOrEmpty(number)) return false;
        number = mobile.ToMobile();
        if (string.IsNullOrEmpty(number)) return false;
        return true;
    }
    public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
    {
        return listToClone.Select(item => (T)item.Clone()).ToList();
    }
    public static string ConvertToPersianNumber(this object Objstr)
    {
        var str = Objstr?.ToString();
        if (string.IsNullOrEmpty(str)) return null;
        string ret = string.Empty;
        for (int i = 0; i < str.Length; i++)
        {
            int ascii = ConvertToASCII(str[i]);
            if (ascii >= 48 && ascii <= 57)
            {
                ret += ((char)(ascii + (1776 - 48))).ToString();
            }
            else
            {
                ret += str[i];
            }
        }
        return ret;
    }
    public static string ConvertToGeorgianNumber(this object Objstr, bool JustNumbers = false)
    {
        var str = Objstr?.ToString();
        if (string.IsNullOrEmpty(str)) return null;
        string ret = string.Empty;
        for (int i = 0; i < str.Length; i++)
        {
            int ascii = ConvertToASCII(str[i]);
            if (ascii >= 1776 && ascii <= 1787)
            {
                ret += ((char)(ascii - (1776 - 48))).ToString();
            }
            else if (!JustNumbers || (JustNumbers && (ascii >= 48 && ascii <= 57)))
            {
                ret += str[i];
            }
        }
        return ret;
    }
    public static int ConvertToASCII(char ch)
    {
        return ch;
    }
    public static string GetSiteName(this string site)
    {
        site = site.Replace("https://", "").Replace("http://", "").Replace("www.", "");
        if (site.IndexOf(":") > -1)
            site = site.Substring(0, site.IndexOf(":"));
        return site;
    }
    public static DateTime StartOfDay(this DateTime date, int hour = 0)
    {
        return new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
    }
    public static string GetRandomNumber(this string str, int count = 4, string chars = "123456789")
    {
        var random = new Random();
        return new string(
            Enumerable.Repeat(chars, count)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
    }
    public static string UrlEncode(this string str)
    {
        return HttpUtility.UrlEncode(str);
    }
    public static string UrlDecode(this string str)
    {
        return HttpUtility.UrlDecode(str);
    }
    public static string ToSqlDate(this DateTime date)
    {
        return date.ToString("yyyy-MM-dd HH:mm:ss");
    }
    public static bool IsUnicode(this string input)
    {
        const int MaxAnsiCode = 255;
        return input.Any(c => c > MaxAnsiCode);
    }
    public static string ConvertToValidFileName(this string input, string defaultFileName)
    {
        if (string.IsNullOrWhiteSpace(input))
            return defaultFileName;

        // 1. حذف کاراکترهای غیرمجاز
        string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
        string pattern = $"[{invalidChars}]";
        string clean = Regex.Replace(input, pattern, "");

        // 2. جایگزینی فاصله‌ها و کاراکترهای خاص
        clean = clean.Replace(" ", "_");
        clean = clean.Replace("‌", ""); // حذف نیم‌فاصله
        clean = clean.Replace("،", ""); // ویرگول فارسی
        clean = clean.Replace("؛", "");
        clean = clean.Replace("؟", "");

        // 3. اختیاری: کوتاه کردن نام خیلی بلند
        if (clean.Length > 100)
            clean = clean.Substring(0, 100);

        // 4. اگر بعد از پاکسازی چیزی نماند
        if (string.IsNullOrWhiteSpace(clean))
            return defaultFileName;

        return clean;
    }
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}
