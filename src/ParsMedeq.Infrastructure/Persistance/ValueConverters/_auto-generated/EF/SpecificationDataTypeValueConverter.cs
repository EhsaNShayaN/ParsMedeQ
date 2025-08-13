global using ParsMedeQ.Domain.Types.SpecificationTypes;

namespace ParsMedeQ.Infrastructure.Persistance.ValueConverters;
sealed class SpecificationDataTypeValueConverter : ValueConverter<SpecificationDataType, string>
{
	public SpecificationDataTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => SpecificationDataType.FromDb(value)
	){}
}


sealed class SpecificationDataTypeValueComparer : ValueComparer<SpecificationDataType>
{
	public SpecificationDataTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

