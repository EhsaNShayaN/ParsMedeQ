namespace SRH.ValueObjects.Money;

public readonly partial record struct MoneyInfo
{
    public bool IsNoMoney() => this.Equals(NoMoney) || Value < 0;
    public bool IsZero() => this.Equals(Zero) || Value.Equals(0);
    public bool IsValid() => !IsNoMoney();
    public bool IsValidAndPositive() => IsValid() && !IsZero() && Value > 0;

    public MoneyInfo Add(MoneyInfo other) =>
        IsZero() ? other
        : other.IsZero() ? this
        : IsSameCurrency(other) ? Create(Value + other.Value, Currency)
        : FailWithCurrencyMismatch("add");

    public MoneyInfo Subtract(MoneyInfo other)
    {
        var sameCurrency = IsSameCurrency(other);
        return other.IsZero() ? this
        : CompareNoLessThan(other) ? Create(Value - other.Value, Currency)
        : sameCurrency ? FailWithInsufficientFunds()
        : FailWithCurrencyMismatch("subtract");
    }

    public MoneyInfo Scale(int factor) => Scale(Convert.ToDecimal(factor));

    public MoneyInfo Scale(decimal factor) =>
        factor < 0 ? FailWithNegativeFactor()
        : Create(Value * factor, Currency);

    public MoneyInfo Div(int factor) => Div(Convert.ToDecimal(factor));
    
    public MoneyInfo Div(decimal factor) =>
       factor <= 0 ? FailWithZeroOrNegativeFactor()
       : new MoneyInfo(Value / factor, Currency);

    public static MoneyInfo Add(MoneyInfo left, MoneyInfo right) => left.Add(right);
    
    public static MoneyInfo Accumulate(MoneyInfo seed, MoneyInfo[] src, Func<MoneyInfo, MoneyInfo, MoneyInfo> func)
    {
        var first = src.First();
        var currency = first.Currency;
        return src.Aggregate(seed, (result, current) => func(result, current));
    }
    
    public static MoneyInfo AccumulateAdd(MoneyInfo[] src)
    {
        return Accumulate(Zero, src, Add);
    }
}
