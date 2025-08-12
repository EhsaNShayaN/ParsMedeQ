global using ParsMedeq.Domain.Types.ProfileTypes;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class ProfileMobileTypeValueConverter : ValueConverter<ProfileMobileType, string>
{
	public ProfileMobileTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => ProfileMobileType.FromDb(value)
	){}
}


sealed class ProfileMobileTypeValueComparer : ValueComparer<ProfileMobileType>
{
	public ProfileMobileTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

