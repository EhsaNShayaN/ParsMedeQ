global using EShop.Domain.Types.ProductTypes;

namespace EShop.Infrastructure.Persistance.ValueConverters;
sealed class ProductVideosTypeValueConverter : ValueConverter<ProductVideosType, string>
{
	public ProductVideosTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => ProductVideosType.FromDb(value)
	){}
}


sealed class ProductVideosTypeValueComparer : ValueComparer<ProductVideosType>
{
	public ProductVideosTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

