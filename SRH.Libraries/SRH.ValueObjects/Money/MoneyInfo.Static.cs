namespace SRH.ValueObjects.Money;

public readonly partial record struct MoneyInfo
{
    public const string NoMoneyText = "[Null]";
    public const string ZeroMoneyText = "[Zero]";

    public readonly static MoneyInfo Zero = new(0, CurrencyInfo.Empty);
    public readonly static MoneyInfo NoMoney = new(-1, CurrencyInfo.Empty);


    private static MoneyInfo FailWithCurrencyMismatch(string operation) => throw new InvalidOperationException($"Can not {operation} by a negative factor");
    private static MoneyInfo FailWithInsufficientFunds() => throw new InvalidOperationException("Can not subtract more MoneyInfo than available");
    private static MoneyInfo FailWithNegativeFactor() => throw new InvalidOperationException("Can not multiply by a negative factor");
    private static MoneyInfo FailWithZeroOrNegativeFactor() => throw new InvalidOperationException("Can not divide by a zero or negative factor");
    private static MoneyInfo FailWithNegativeValue() => throw new InvalidOperationException("Can not create negative money");
    public static bool Equals(MoneyInfo? a, MoneyInfo? b)
    {
        var x = a ?? NoMoney;
        var y = b ?? NoMoney;
        return x.IsSameCurrency(y) && x.Value.Equals(y.Value);
    }
}
