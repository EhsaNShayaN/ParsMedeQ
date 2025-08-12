namespace ParsMedeq.Domain.Helpers;
public static class DecimalValueHelper
{
    internal const int MAX_VALE_3 = 1_000;
    internal const int MAX_VALE_4 = 10_000;
    internal const ulong MAX_VALE_14 = 100_000_000_000_000;
    internal const ulong MAX_VALE_15 = 1_000_000_000_000_000;
    internal const ulong MAX_VALE_18 = 1_000_000_000_000_000_000;

    public const string ColumnType_15_8 = "decimal(23,8)";
    public const string ColumnType_18_8 = "decimal(26,8)";
    public const string ColumnType_18_0 = "decimal(18,0)";
    public const string ColumnType_14_4 = "decimal(18,4)";

    public static bool IsValid3(decimal value) => value >= 0 && value < MAX_VALE_3;
    public static bool IsValid4(decimal value) => value >= 0 && value < MAX_VALE_4;
    public static bool IsValid14(decimal value) => value >= 0 && value < MAX_VALE_14;
    public static bool IsValid14(ulong value) => value >= 0 && value < MAX_VALE_14;
    public static bool IsValid15(ulong value) => value >= 0 && value < MAX_VALE_15;
    public static bool IsValid15(decimal value) => value >= 0 && value < MAX_VALE_15;
    public static bool IsValid18(decimal value) => value >= 0 && value < MAX_VALE_18;
    public static bool IsValid18(ulong value) => value >= 0 && value < MAX_VALE_18;

    static decimal Round(decimal value, int decimals = 0) => decimal.Round(value, decimals);
    public static decimal Round0(decimal value) => Round(value, 0);
    public static decimal Round2(decimal value) => Round(value, 2);
    public static decimal Round4(decimal value) => Round(value, 4);
    public static decimal Round8(decimal value) => Round(value, 8);
}
