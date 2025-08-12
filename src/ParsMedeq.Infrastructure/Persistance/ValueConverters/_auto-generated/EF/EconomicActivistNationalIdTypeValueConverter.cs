global using ParsMedeq.Domain.Types.EconomicActivistNationalId;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class EconomicActivistNationalIdTypeValueConverter : ValueConverter<EconomicActivistNationalIdType, string>
{
	public EconomicActivistNationalIdTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => EconomicActivistNationalIdType.FromDb(value)
	){}
}


sealed class EconomicActivistNationalIdTypeValueComparer : ValueComparer<EconomicActivistNationalIdType>
{
	public EconomicActivistNationalIdTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

