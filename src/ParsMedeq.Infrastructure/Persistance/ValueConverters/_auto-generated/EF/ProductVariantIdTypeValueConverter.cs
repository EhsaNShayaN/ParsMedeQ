global using ParsMedeq.Domain.Types.ProductVariantTypes;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class ProductVariantIdTypeValueConverter : ValueConverter<ProductVariantIdType, int>
{
	public ProductVariantIdTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => ProductVariantIdType.FromDb(value)
	){}
}


sealed class ProductVariantIdTypeValueComparer : ValueComparer<ProductVariantIdType>
{
	public ProductVariantIdTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

