global using ParsMedeQ.Domain.Types.EconomicActivistType;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class EconomicActivistTypeInfoDapperTypeMapper : SqlMapper.TypeHandler<EconomicActivistTypeInfo>
{
	public override EconomicActivistTypeInfo Parse(object value) => EconomicActivistTypeInfo.FromId(Convert.ToByte(value));
	public override void SetValue(IDbDataParameter parameter, EconomicActivistTypeInfo value) => parameter.Value = value.GetDbValue();
}


