global using ParsMedeQ.Domain.Types.BrandTypes;

namespace ParsMedeQ.Infrastructure.Persistance.ValueConverters;
sealed class BrandTitleTypeValueConverter : ValueConverter<BrandTitleType, string>
{
	public BrandTitleTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => BrandTitleType.FromDb(value)
	){}
}


sealed class BrandTitleTypeValueComparer : ValueComparer<BrandTitleType>
{
	public BrandTitleTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

