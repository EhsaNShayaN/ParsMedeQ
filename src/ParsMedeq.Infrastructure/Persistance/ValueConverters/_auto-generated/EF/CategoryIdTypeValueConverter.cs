global using ParsMedeq.Domain.Types.CategoryTypes;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class CategoryIdTypeValueConverter : ValueConverter<CategoryIdType, int>
{
	public CategoryIdTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => CategoryIdType.FromDb(value)
	){}
}


sealed class CategoryIdTypeValueComparer : ValueComparer<CategoryIdType>
{
	public CategoryIdTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

