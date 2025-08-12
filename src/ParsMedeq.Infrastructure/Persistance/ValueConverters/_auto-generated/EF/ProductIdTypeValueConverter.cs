global using ParsMedeq.Domain.Types.ProductTypes;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class ProductIdTypeValueConverter : ValueConverter<ProductIdType, int>
{
	public ProductIdTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => ProductIdType.FromDb(value)
	){}
}


sealed class ProductIdTypeValueComparer : ValueComparer<ProductIdType>
{
	public ProductIdTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

