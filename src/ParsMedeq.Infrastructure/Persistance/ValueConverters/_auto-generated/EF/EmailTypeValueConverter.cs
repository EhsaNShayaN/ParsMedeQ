global using ParsMedeQ.Domain.Types.Email;

namespace ParsMedeQ.Infrastructure.Persistance.ValueConverters;
sealed class EmailTypeValueConverter : ValueConverter<EmailType, string>
{
	public EmailTypeValueConverter(): base(
		src => src.GetDbValue(),
		value => EmailType.FromDb(value)
	){}
}


sealed class EmailTypeValueComparer : ValueComparer<EmailType>
{
	public EmailTypeValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

