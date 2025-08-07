using System.Globalization;

namespace SRH.DateProvider;

public class DateProvider : IDateProvider
{
    public readonly static DateProvider Instance = new DateProvider();

    public const string Default_PersianDate_Format = "yyyy/MM/dd";
    public const string Default_PersianDateTime_Format = "yyyy/MM/dd HH:mm:ss";
    public static CultureInfo PersianCulture = CultureInfo.CreateSpecificCulture("fa-IR");
    public static CultureInfo GregorianCulture = CultureInfo.CreateSpecificCulture("en-US");
    public const string IranStandardTime = "Iran Standard Time";

    public DateTimeOffset Now => DateTimeOffset.UtcNow;

    #region " ConvertTime "
    public DateTimeOffset ConvertTime(DateTimeOffset dt, TimeZoneInfo destinationTimeZone)
        => TimeZoneInfo.ConvertTime(dt, destinationTimeZone);

    public DateTimeOffset ConvertTime(DateTime dt, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
        => this.ToDateTimeOffset(
            TimeZoneInfo.ConvertTime(
                DateTime.SpecifyKind(dt, DateTimeKind.Unspecified),
                GetTimezoneInfoOfDateTime(dt, sourceTimeZone),
                destinationTimeZone),
            destinationTimeZone);

    public DateTimeOffset ConvertTime(DateTime dt, TimeZoneInfo destinationTimeZone)
        => this.ConvertTime(
            dt,
            GetTimezoneInfoOfDateTime(dt, TimeZoneInfo.Local),
            destinationTimeZone);

    public DateTimeOffset ConvertTime(DateTime dt, string sourceTimeZone, string destinationTimeZone)
        => this.ConvertTime(dt,
             GetTimezoneInfoOfDateTime(dt, sourceTimeZone),
             GetTimezoneInfoByName(destinationTimeZone));

    public DateTimeOffset ConvertTime(DateTimeOffset dt, string destinationTimeZone)
        => TimeZoneInfo.ConvertTime(dt, GetTimezoneInfoByName(destinationTimeZone));

    public DateTimeOffset ConvertFromIranTime(DateTime dt) => ConvertTime(dt, IranStandardTime);

    public DateTimeOffset ConvertFromIranTime(DateOnly dt) => ConvertTime(dt.ToDateTime(new TimeOnly(5, 0)), IranStandardTime);
    #endregion

    #region " ConvertToUtc "
    public DateTimeOffset ConvertToUtc(DateTimeOffset dt) => TimeZoneInfo.ConvertTimeToUtc(dt.DateTime);
    public DateTimeOffset ConvertToUtc(DateTime dt) => TimeZoneInfo.ConvertTimeToUtc(dt);
    public DateTimeOffset ConvertToUtc(DateTime dt, TimeZoneInfo sourceTimeZone) => TimeZoneInfo.ConvertTimeToUtc(dt, sourceTimeZone);
    public DateTimeOffset ConvertToUtc(DateTime dt, string destinationTimeZone) => TimeZoneInfo.ConvertTimeToUtc(dt, GetTimezoneInfoByName(destinationTimeZone));
    #endregion

    #region " ConvertFromUtc " 
    public DateTimeOffset ConvertFromUtc(DateTime dt, TimeZoneInfo destinationTimeZone)
    => ToDateTimeOffset(TimeZoneInfo.ConvertTimeFromUtc(dt, destinationTimeZone), destinationTimeZone);
    public DateTimeOffset ConvertFromUtc(DateTime dt, string destinationTimeZone) => this.ConvertFromUtc(dt, GetTimezoneInfoByName(destinationTimeZone));
    public DateTimeOffset ConvertFromUtc(DateTime dt) => this.ConvertFromUtc(dt, TimeZoneInfo.Local);

    public DateTimeOffset ConvertFromUtc(DateTimeOffset dt, TimeZoneInfo destinationTimeZone) => this.ConvertFromUtc(dt.DateTime, destinationTimeZone);
    public DateTimeOffset ConvertFromUtc(DateTimeOffset dt, string destinationTimeZone) => this.ConvertFromUtc(dt.DateTime, GetTimezoneInfoByName(destinationTimeZone));
    public DateTimeOffset ConvertFromUtc(DateTimeOffset dt) => this.ConvertFromUtc(dt.DateTime, TimeZoneInfo.Local);
    #endregion

    #region " ToPersianDate "
    public string ToPersianDate(DateTime dt, string sourceTimezone, string format = Default_PersianDate_Format)
        => this.ConvertTime(dt, sourceTimezone, IranStandardTime).ToString(format, PersianCulture);
    public string ToPersianDate(DateTimeOffset dt, string format = Default_PersianDate_Format)
        => this.ConvertTime(dt, IranStandardTime).ToString(format, PersianCulture);
    public string ToPersianDate(DateTime dt, string format = Default_PersianDate_Format) => dt.ToString(format, PersianCulture);
    public string ToPersianDate(DateOnly dt, string format = Default_PersianDate_Format) => dt.ToString(format, PersianCulture);
    #endregion

    public DateTime ToGregorianDate(string date, CultureInfo? currentCulture = null)
    {
        if (date.Contains("/")) return DateTime.Parse(date, currentCulture ?? PersianCulture);
        if (date.Length == 8) return DateTime.Parse($"{date[..4]}/{date[4..6]}/{date[^2..]}", currentCulture ?? PersianCulture);
        return DateTime.MinValue;
    }
    public DateTimeOffset ToGregorianDateFromIranTime(string date, CultureInfo? currentCulture = null) =>
        this.ConvertFromIranTime(
            this.ToGregorianDate(date, currentCulture));

    //=> DateTimeOffset.Parse(date, currentCulture ?? PersianCulture).DateTime;

    public string ShamsiOrMiladiToShamsi(string date)
    {
        var (d, year, time, shamsiOrMiladi) = this.GetDateInfo(date);
        if (shamsiOrMiladi.Equals("shamsi", StringComparison.InvariantCultureIgnoreCase)) return d;
        if (shamsiOrMiladi.Equals("miladi", StringComparison.InvariantCultureIgnoreCase)) return this.ToPersianDate(DateTime.Parse(d), Default_PersianDate_Format);
        return string.Empty;
    }

    public DateTime ShamsiOrMiladiToMiladi(string date)
    {
        var (d, year, time, shamsiOrMiladi) = this.GetDateInfo(date);
        if (shamsiOrMiladi.Equals("shamsi", StringComparison.InvariantCultureIgnoreCase)) return this.ToGregorianDate(d, null) + time;
        if (shamsiOrMiladi.Equals("miladi", StringComparison.InvariantCultureIgnoreCase)) return DateTime.Parse(d) + time;
        return DateTime.MinValue;
    }

    #region " Private Methods "
    private static TimeZoneInfo GetTimezoneInfoByName(string desiredTimezoneName) => TimeZoneConverter.TZConvert.GetTimeZoneInfo(desiredTimezoneName);
    private TimeZoneInfo GetTimezoneInfoOfDateTime(DateTime dt, TimeZoneInfo desiredTimezone)
        => dt.Kind switch
        {
            DateTimeKind.Local => TimeZoneInfo.Local,
            DateTimeKind.Utc => TimeZoneInfo.Utc,
            _ => desiredTimezone
        };
    private TimeZoneInfo GetTimezoneInfoOfDateTime(DateTime dt, string desiredTimezoneName)
        => dt.Kind switch
        {
            DateTimeKind.Local => TimeZoneInfo.Local,
            DateTimeKind.Utc => TimeZoneInfo.Utc,
            _ => GetTimezoneInfoByName(desiredTimezoneName)
        };
    private DateTimeOffset ToDateTimeOffset(DateTime dt, TimeZoneInfo destinationTimeZone) => new DateTimeOffset(dt, destinationTimeZone.GetUtcOffset(dt));
    private (string d, int year, TimeSpan time, string shamsiOrMiladi) GetDateInfo(string date)
    {
        if (string.IsNullOrWhiteSpace(date ?? "")) return (string.Empty, 0, TimeSpan.MinValue, "UNKNOWN");

        var dateParts = date!.Split(" ");
        var time = dateParts.Length > 1 ? DateTimeOffset.Parse(date).TimeOfDay : TimeSpan.Zero;

        var datePart = dateParts.First();
        if (datePart.Length != 8 && datePart.Length != 10)
        {
            return (string.Empty, 0, TimeSpan.MinValue, "UNKNOWN");
        }

        var tenCharsDate = $"{this.ToPersianDate(DateTime.Now, "yyyy")[0..2]}{datePart}"[^10..];

        var parts = (tenCharsDate!.Split(new char[] { '-', '/' }) ?? Array.Empty<string>()).AsSpan();
        if (parts.IsEmpty || parts.Length != 3) return (tenCharsDate!, 0, TimeSpan.MinValue, "UNKNOWN");

        if (int.TryParse(parts[0][^4..], out int year))
        {
            var d = $"{parts[0][^4..]}/{$"0{parts[1]}"[^2..]}/{$"0{parts[2]}"[^2..]}";
            if (year >= 1700)
            {
                if (DateTime.TryParse(d, out _)) return (d, year, time, "miladi");
                return (d!, year, TimeSpan.MinValue, "UNKNOWN");
            }
            return (d, year, time, "shamsi");
        }

        return (string.Empty, 0, TimeSpan.MinValue, string.Empty);
    }
    #endregion
}