global using ParsMedeq.Domain.Types.BrandTypes;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class BrandNameTypeValueConverter : ValueConverter<BrandNameType, string>
{
	public BrandNameTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => BrandNameType.FromDb(value)
	){}
}


sealed class BrandNameTypeValueComparer : ValueComparer<BrandNameType>
{
	public BrandNameTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

