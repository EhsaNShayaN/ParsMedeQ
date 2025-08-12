global using ParsMedeq.Domain.Types.LastName;

namespace ParsMedeq.Infrastructure.Persistance.DapperValueConverters;
sealed class LastNameTypeDapperTypeMapper : SqlMapper.TypeHandler<LastNameType>
{
	public override LastNameType Parse(object value) => LastNameType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, LastNameType value) => parameter.Value = value.GetDbValue();
}


