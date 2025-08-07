global using EShop.Domain.Types.Mobile;

namespace EShop.Infrastructure.Persistance.ValueConverters;
sealed class MobileTypeValueConverter : ValueConverter<MobileType, string>
{
	public MobileTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => MobileType.FromDb(value)
	){}
}


sealed class MobileTypeValueComparer : ValueComparer<MobileType>
{
	public MobileTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

