namespace SRH.ValueObjects.Money;

public readonly partial record struct MoneyInfo
{
    public readonly decimal Value { get; }
    public readonly CurrencyInfo Currency { get; }

    private MoneyInfo(decimal value, CurrencyInfo currency)
    {
        Value = value;
        Currency = currency;
    }

    #region " Factory Methods "
    public static MoneyInfo Create(decimal? value, CurrencyInfo currency) =>
        value is null
        ? NoMoney
        : value < 0
            ? FailWithNegativeValue()
            : value!.Value.CompareTo(0) == 0
                ? Zero
                : new(decimal.Round(value!.Value, currency.MinorUnit), currency);
    public static MoneyInfo Create(decimal? value, string currencyCode) => Create(value, CurrencyInfo.FromCode(currencyCode));
    public static MoneyInfo CreateFromDouble(double? value, CurrencyInfo currency) => Create(value is null ? null : Convert.ToDecimal(value!.Value), currency);
    public static MoneyInfo CreateFromDouble(double? value, string currencyCode) => Create(value is null ? null : Convert.ToDecimal(value!.Value), CurrencyInfo.FromCode(currencyCode));
    public static MoneyInfo Create(string value)
    {
        if (value.Equals(NoMoneyText)) return NoMoney;

        if (value.Equals(ZeroMoneyText)) return Zero;

        var parts = value.Split(' ');
        return Create(Convert.ToDecimal(parts[0].Replace(",", string.Empty)), parts[1]);
    }
    #endregion

    private bool CompareNoLessThan(MoneyInfo other) => IsSameCurrency(other) && Value >= other.Value;
    private bool IsSameCurrency(MoneyInfo other) => Currency.Equals(other.Currency);

    public override string ToString()
    {
        if (IsNoMoney()) return NoMoneyText;

        if (IsZero()) return ZeroMoneyText;

        return $"{Value.ToString($"N{Currency.MinorUnit}")} {Currency.Code}";
    }
}