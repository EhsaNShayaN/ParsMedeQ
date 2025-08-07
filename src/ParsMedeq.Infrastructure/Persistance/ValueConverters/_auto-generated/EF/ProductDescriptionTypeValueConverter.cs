global using EShop.Domain.Types.ProductTypes;

namespace EShop.Infrastructure.Persistance.ValueConverters;
sealed class ProductDescriptionTypeValueConverter : ValueConverter<ProductDescriptionType, string>
{
	public ProductDescriptionTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => ProductDescriptionType.FromDb(value)
	){}
}


sealed class ProductDescriptionTypeValueComparer : ValueComparer<ProductDescriptionType>
{
	public ProductDescriptionTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

