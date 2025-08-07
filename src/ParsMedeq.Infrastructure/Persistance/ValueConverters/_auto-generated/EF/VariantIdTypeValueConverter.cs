global using EShop.Domain.Types.VariantTypes;

namespace EShop.Infrastructure.Persistance.ValueConverters;
sealed class VariantIdTypeValueConverter : ValueConverter<VariantIdType, int>
{
	public VariantIdTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => VariantIdType.FromDb(value)
	){}
}


sealed class VariantIdTypeValueComparer : ValueComparer<VariantIdType>
{
	public VariantIdTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

