namespace SRH.ValueObjects.Currency;

public readonly partial record struct CurrencyInfo
{
    public readonly string Code { get; }
    public readonly string Name { get; }
    public readonly string PersianName { get; }
    public readonly string Symbol { get; }
    public readonly int MinorUnit { get; }

    private CurrencyInfo(string code, string name, string persianName, string symbol, int minorUnit)
    {
        Code = code;
        Name = name;
        PersianName = persianName;
        Symbol = symbol;
        MinorUnit = minorUnit;
    }
}