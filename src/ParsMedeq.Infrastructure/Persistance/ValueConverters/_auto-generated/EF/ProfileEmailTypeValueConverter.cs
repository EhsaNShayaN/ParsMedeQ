global using ParsMedeq.Domain.Types.ProfileTypes;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class ProfileEmailTypeValueConverter : ValueConverter<ProfileEmailType, string>
{
	public ProfileEmailTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => ProfileEmailType.FromDb(value)
	){}
}


sealed class ProfileEmailTypeValueComparer : ValueComparer<ProfileEmailType>
{
	public ProfileEmailTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

