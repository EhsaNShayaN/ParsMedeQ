global using ParsMedeq.Domain.Types.ProfileTypes;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class ProfileFirstNameTypeValueConverter : ValueConverter<ProfileFirstNameType, string>
{
	public ProfileFirstNameTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => ProfileFirstNameType.FromDb(value)
	){}
}


sealed class ProfileFirstNameTypeValueComparer : ValueComparer<ProfileFirstNameType>
{
	public ProfileFirstNameTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

