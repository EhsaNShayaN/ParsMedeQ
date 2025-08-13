global using ParsMedeQ.Domain.Types.BrandTypes;

namespace ParsMedeQ.Infrastructure.Persistance.ValueConverters;
sealed class BrandIdTypeValueConverter : ValueConverter<BrandIdType, int>
{
	public BrandIdTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => BrandIdType.FromDb(value)
	){}
}


sealed class BrandIdTypeValueComparer : ValueComparer<BrandIdType>
{
	public BrandIdTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

