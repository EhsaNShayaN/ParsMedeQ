global using ParsMedeq.Domain.Types.SpecificationTypes;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class SpecificationUnitTypeValueConverter : ValueConverter<SpecificationUnitType, string>
{
	public SpecificationUnitTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => SpecificationUnitType.FromDb(value)
	){}
}


sealed class SpecificationUnitTypeValueComparer : ValueComparer<SpecificationUnitType>
{
	public SpecificationUnitTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

