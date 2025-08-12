global using ParsMedeq.Domain.Types.CategoryTypes;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class CategoryTitleTypeValueConverter : ValueConverter<CategoryTitleType, string>
{
	public CategoryTitleTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => CategoryTitleType.FromDb(value)
	){}
}


sealed class CategoryTitleTypeValueComparer : ValueComparer<CategoryTitleType>
{
	public CategoryTitleTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

