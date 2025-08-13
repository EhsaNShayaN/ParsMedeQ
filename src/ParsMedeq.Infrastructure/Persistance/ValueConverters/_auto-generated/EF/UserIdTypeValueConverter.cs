global using ParsMedeQ.Domain.Types.UserId;

namespace ParsMedeQ.Infrastructure.Persistance.ValueConverters;
sealed class UserIdTypeValueConverter : ValueConverter<UserIdType, int>
{
	public UserIdTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => UserIdType.FromDb(value)
	){}
}


sealed class UserIdTypeValueComparer : ValueComparer<UserIdType>
{
	public UserIdTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

