global using ParsMedeq.Domain.Types.ProfileTypes;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class ProfileIdTypeValueConverter : ValueConverter<ProfileIdType, int>
{
	public ProfileIdTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => ProfileIdType.FromDb(value)
	){}
}


sealed class ProfileIdTypeValueComparer : ValueComparer<ProfileIdType>
{
	public ProfileIdTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

