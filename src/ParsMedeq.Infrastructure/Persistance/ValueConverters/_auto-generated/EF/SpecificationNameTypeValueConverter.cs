global using ParsMedeQ.Domain.Types.SpecificationTypes;

namespace ParsMedeQ.Infrastructure.Persistance.ValueConverters;
sealed class SpecificationNameTypeValueConverter : ValueConverter<SpecificationNameType, string>
{
	public SpecificationNameTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => SpecificationNameType.FromDb(value)
	){}
}


sealed class SpecificationNameTypeValueComparer : ValueComparer<SpecificationNameType>
{
	public SpecificationNameTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

