using SRH.ValueObjects.Currency;

namespace ParsMedeQ.Infrastructure.Persistance.ValueConverters;

sealed class CurrencyInfoValueConverter : ValueConverter<CurrencyInfo, string>
{
    public CurrencyInfoValueConverter() : base(
        src => src.Code,
        value => CurrencyInfo.FromCode(value)
    )
    { }
}
sealed class CurrencyInfoValueComparer : ValueComparer<CurrencyInfo>
{
    public CurrencyInfoValueComparer() : base(
        (a, b) => a.Equals(b),
        a => a.GetHashCode())
    { }
}
