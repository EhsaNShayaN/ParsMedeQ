global using ParsMedeq.Domain.Types.Email;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
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

