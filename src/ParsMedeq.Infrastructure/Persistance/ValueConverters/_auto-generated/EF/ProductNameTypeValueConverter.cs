global using ParsMedeq.Domain.Types.ProductTypes;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class ProductNameTypeValueConverter : ValueConverter<ProductNameType, string>
{
	public ProductNameTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => ProductNameType.FromDb(value)
	){}
}


sealed class ProductNameTypeValueComparer : ValueComparer<ProductNameType>
{
	public ProductNameTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

