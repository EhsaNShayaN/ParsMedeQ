global using EShop.Domain.Types.Mobile;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class MobileTypeDapperTypeMapper : SqlMapper.TypeHandler<MobileType>
{
	public override MobileType Parse(object value) => MobileType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, MobileType value) => parameter.Value = value.GetDbValue();
}


