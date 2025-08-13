global using ParsMedeQ.Domain.Types.FirstName;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class FirstNameTypeDapperTypeMapper : SqlMapper.TypeHandler<FirstNameType>
{
	public override FirstNameType Parse(object value) => FirstNameType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, FirstNameType value) => parameter.Value = value.GetDbValue();
}


