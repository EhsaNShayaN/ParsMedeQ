global using ParsMedeQ.Domain.Types.ProfileTypes;

namespace ParsMedeQ.Infrastructure.Persistance.ValueConverters;
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

