namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class ProductSpecificationIdTypeValueConverter : ValueConverter<ProductSpecificationIdType, int>
{
	public ProductSpecificationIdTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => ProductSpecificationIdType.FromDb(value)
	){}
}


sealed class ProductSpecificationIdTypeValueComparer : ValueComparer<ProductSpecificationIdType>
{
	public ProductSpecificationIdTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

