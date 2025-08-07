global using EShop.Domain.Types.BrandTypes;

namespace EShop.Infrastructure.Persistance.ValueConverters;
sealed class BrandImageTypeValueConverter : ValueConverter<BrandImageType, string>
{
	public BrandImageTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => BrandImageType.FromDb(value)
	){}
}


sealed class BrandImageTypeValueComparer : ValueComparer<BrandImageType>
{
	public BrandImageTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

