global using ParsMedeQ.Domain.Types.Mobile;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class MobileTypeDapperTypeMapper : SqlMapper.TypeHandler<MobileType>
{
	public override MobileType Parse(object value) => MobileType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, MobileType value) => parameter.Value = value.GetDbValue();
}


