global using ParsMedeQ.Domain.Types.ProfileTypes;

namespace ParsMedeQ.Infrastructure.Persistance.ValueConverters;
sealed class ProfileLastNameTypeValueConverter : ValueConverter<ProfileLastNameType, string>
{
	public ProfileLastNameTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => ProfileLastNameType.FromDb(value)
	){}
}


sealed class ProfileLastNameTypeValueComparer : ValueComparer<ProfileLastNameType>
{
	public ProfileLastNameTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

