global using ParsMedeq.Domain.Types.ProductTypes;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class ProductImagesTypeValueConverter : ValueConverter<ProductImagesType, string>
{
	public ProductImagesTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => ProductImagesType.FromDb(value)
	){}
}


sealed class ProductImagesTypeValueComparer : ValueComparer<ProductImagesType>
{
	public ProductImagesTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

