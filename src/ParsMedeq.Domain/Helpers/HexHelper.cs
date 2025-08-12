namespace ParsMedeq.Domain.Helpers;

public static class HexHelper
{
    public static string ConvertToHexString(int value) => Convert.ToString(value, 16);
    public static string ConvertToHexString(long value) => Convert.ToString(value, 16);
}