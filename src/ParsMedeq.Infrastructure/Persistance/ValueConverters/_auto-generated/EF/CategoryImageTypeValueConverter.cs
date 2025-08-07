global using EShop.Domain.Types.CategoryTypes;

namespace EShop.Infrastructure.Persistance.ValueConverters;
sealed class CategoryImageTypeValueConverter : ValueConverter<CategoryImageType, string>
{
	public CategoryImageTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => CategoryImageType.FromDb(value)
	){}
}


sealed class CategoryImageTypeValueComparer : ValueComparer<CategoryImageType>
{
	public CategoryImageTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

