global using EShop.Domain.Types.EconomicActivistType;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class EconomicActivistTypeInfoDapperTypeMapper : SqlMapper.TypeHandler<EconomicActivistTypeInfo>
{
	public override EconomicActivistTypeInfo Parse(object value) => EconomicActivistTypeInfo.FromId(Convert.ToByte(value));
	public override void SetValue(IDbDataParameter parameter, EconomicActivistTypeInfo value) => parameter.Value = value.GetDbValue();
}


