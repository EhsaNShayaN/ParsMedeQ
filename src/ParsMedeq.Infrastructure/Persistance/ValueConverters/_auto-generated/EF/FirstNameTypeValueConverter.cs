global using ParsMedeQ.Domain.Types.FirstName;

namespace ParsMedeQ.Infrastructure.Persistance.ValueConverters;
sealed class FirstNameTypeValueConverter : ValueConverter<FirstNameType, string>
{
	public FirstNameTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => FirstNameType.FromDb(value)
	){}
}


sealed class FirstNameTypeValueComparer : ValueComparer<FirstNameType>
{
	public FirstNameTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

