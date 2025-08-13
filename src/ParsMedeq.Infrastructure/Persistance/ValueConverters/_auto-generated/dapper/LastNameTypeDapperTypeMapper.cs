global using ParsMedeQ.Domain.Types.LastName;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class LastNameTypeDapperTypeMapper : SqlMapper.TypeHandler<LastNameType>
{
	public override LastNameType Parse(object value) => LastNameType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, LastNameType value) => parameter.Value = value.GetDbValue();
}


