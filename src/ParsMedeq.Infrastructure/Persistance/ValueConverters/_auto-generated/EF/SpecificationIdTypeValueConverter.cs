global using EShop.Domain.Types.SpecificationTypes;

namespace EShop.Infrastructure.Persistance.ValueConverters;
sealed class SpecificationIdTypeValueConverter : ValueConverter<SpecificationIdType, int>
{
	public SpecificationIdTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => SpecificationIdType.FromDb(value)
	){}
}


sealed class SpecificationIdTypeValueComparer : ValueComparer<SpecificationIdType>
{
	public SpecificationIdTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

