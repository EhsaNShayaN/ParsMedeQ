global using ParsMedeQ.Domain.Types.Email;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class EmailTypeDapperTypeMapper : SqlMapper.TypeHandler<EmailType>
{
	public override EmailType Parse(object value) => EmailType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, EmailType value) => parameter.Value = value.GetDbValue();
}


