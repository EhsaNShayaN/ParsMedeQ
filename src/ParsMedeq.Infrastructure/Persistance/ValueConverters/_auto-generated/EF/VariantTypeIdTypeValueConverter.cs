global using ParsMedeQ.Domain.Types.VariantTypes;

namespace ParsMedeQ.Infrastructure.Persistance.ValueConverters;
sealed class VariantTypeIdTypeValueConverter : ValueConverter<VariantTypeIdType, byte>
{
	public VariantTypeIdTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => VariantTypeIdType.FromId(value)
	){}
}


sealed class VariantTypeIdTypeValueComparer : ValueComparer<VariantTypeIdType>
{
	public VariantTypeIdTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

