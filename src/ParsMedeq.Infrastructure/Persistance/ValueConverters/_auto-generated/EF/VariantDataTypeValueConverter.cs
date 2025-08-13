global using ParsMedeQ.Domain.Types.VariantTypes;

namespace ParsMedeQ.Infrastructure.Persistance.ValueConverters;
sealed class VariantDataTypeValueConverter : ValueConverter<VariantDataType, string>
{
	public VariantDataTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => VariantDataType.FromDb(value)
	){}
}


sealed class VariantDataTypeValueComparer : ValueComparer<VariantDataType>
{
	public VariantDataTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

