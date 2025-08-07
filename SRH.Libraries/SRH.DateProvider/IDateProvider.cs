using System.Globalization;

namespace SRH.DateProvider;

public interface IDateProvider
{
    DateTimeOffset Now { get; }

    DateTimeOffset ConvertFromUtc(DateTime dt);
    DateTimeOffset ConvertFromUtc(DateTime dt, string destinationTimeZone);
    DateTimeOffset ConvertFromUtc(DateTime dt, TimeZoneInfo destinationTimeZone);
    DateTimeOffset ConvertFromUtc(DateTimeOffset dt);
    DateTimeOffset ConvertFromUtc(DateTimeOffset dt, string destinationTimeZone);
    DateTimeOffset ConvertFromUtc(DateTimeOffset dt, TimeZoneInfo destinationTimeZone);
    DateTimeOffset ConvertTime(DateTime dt, string sourceTimeZone, string destinationTimeZone);
    DateTimeOffset ConvertTime(DateTime dt, TimeZoneInfo destinationTimeZone);
    DateTimeOffset ConvertTime(DateTime dt, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone);
    DateTimeOffset ConvertTime(DateTimeOffset dt, string destinationTimeZone);
    DateTimeOffset ConvertTime(DateTimeOffset dt, TimeZoneInfo destinationTimeZone);
    DateTimeOffset ConvertToUtc(DateTime dt);
    DateTimeOffset ConvertToUtc(DateTime dt, string destinationTimeZone);
    DateTimeOffset ConvertToUtc(DateTime dt, TimeZoneInfo sourceTimeZone);
    DateTimeOffset ConvertToUtc(DateTimeOffset dt);
    DateTime ShamsiOrMiladiToMiladi(string date);
    string ShamsiOrMiladiToShamsi(string date);
    DateTime ToGregorianDate(string date, CultureInfo? currentCulture);
    string ToPersianDate(DateTime dt, string format = "yyyy/MM/dd");
    string ToPersianDate(DateTime dt, string sourceTimezone, string format = "yyyy/MM/dd");
    string ToPersianDate(DateTimeOffset dt, string format = "yyyy/MM/dd");
}
