namespace SRH.PrimitiveExtensions.String;

public static class StringExtensions
{
    public static bool HasValue(this string? src) =>
        src is not null
        && !string.IsNullOrWhiteSpace(src)
        && !string.IsNullOrEmpty(src);

    public static bool LengthIsBetween(this string? src, int min, int max) =>
        src.HasValue()
        && src!.Length >= min
        && src!.Length <= max;

    public static bool LengthIsLessThan(this string? src, int length) =>
        src.HasValue()
        && src!.Length < length;

    public static bool LengthIsLessThanOrEquals(this string? src, int length) =>
        src.HasValue()
        && src!.Length <= length;

    public static bool LengthIsGreaterThan(this string? src, int length) =>
       src.HasValue()
       && src!.Length > length;

    public static bool LengthIsGreaterThanOrEquals(this string? src, int length) =>
        src.HasValue()
        && src!.Length >= length;
}
