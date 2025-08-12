global using ParsMedeq.Domain.Types.EconomicActivistType;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class EconomicActivistTypeInfoValueConverter : ValueConverter<EconomicActivistTypeInfo, byte>
{
	public EconomicActivistTypeInfoValueConverter(): base(
		src => src.GetDbValue(),
		value => EconomicActivistTypeInfo.FromId(value)
	){}
}


sealed class EconomicActivistTypeInfoValueComparer : ValueComparer<EconomicActivistTypeInfo>
{
	public EconomicActivistTypeInfoValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

