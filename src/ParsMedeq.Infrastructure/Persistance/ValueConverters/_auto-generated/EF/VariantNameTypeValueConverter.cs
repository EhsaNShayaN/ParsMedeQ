global using ParsMedeQ.Domain.Types.VariantTypes;

namespace ParsMedeQ.Infrastructure.Persistance.ValueConverters;
sealed class VariantNameTypeValueConverter : ValueConverter<VariantNameType, string>
{
	public VariantNameTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => VariantNameType.FromDb(value)
	){}
}


sealed class VariantNameTypeValueComparer : ValueComparer<VariantNameType>
{
	public VariantNameTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

