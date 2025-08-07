global using EShop.Domain.Types.VariantTypes;

namespace EShop.Infrastructure.Persistance.ValueConverters;
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

