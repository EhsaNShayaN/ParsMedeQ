global using ParsMedeQ.Domain.Types.LastName;

namespace ParsMedeQ.Infrastructure.Persistance.ValueConverters;
sealed class LastNameTypeValueConverter : ValueConverter<LastNameType, string>
{
	public LastNameTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => LastNameType.FromDb(value)
	){}
}


sealed class LastNameTypeValueComparer : ValueComparer<LastNameType>
{
	public LastNameTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

