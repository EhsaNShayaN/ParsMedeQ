global using EShop.Domain.Types.ProductTypes;

namespace EShop.Infrastructure.Persistance.ValueConverters;
sealed class ProductTitleTypeValueConverter : ValueConverter<ProductTitleType, string>
{
	public ProductTitleTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => ProductTitleType.FromDb(value)
	){}
}


sealed class ProductTitleTypeValueComparer : ValueComparer<ProductTitleType>
{
	public ProductTitleTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

