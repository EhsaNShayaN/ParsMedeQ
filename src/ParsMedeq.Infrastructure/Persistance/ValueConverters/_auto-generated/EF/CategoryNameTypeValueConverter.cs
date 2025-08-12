global using ParsMedeq.Domain.Types.CategoryTypes;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class CategoryNameTypeValueConverter : ValueConverter<CategoryNameType, string>
{
	public CategoryNameTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => CategoryNameType.FromDb(value)
	){}
}


sealed class CategoryNameTypeValueComparer : ValueComparer<CategoryNameType>
{
	public CategoryNameTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

