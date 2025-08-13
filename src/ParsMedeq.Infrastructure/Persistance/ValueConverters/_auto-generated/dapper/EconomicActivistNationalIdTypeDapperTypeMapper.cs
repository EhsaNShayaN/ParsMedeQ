global using ParsMedeQ.Domain.Types.EconomicActivistNationalId;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class EconomicActivistNationalIdTypeDapperTypeMapper : SqlMapper.TypeHandler<EconomicActivistNationalIdType>
{
	public override EconomicActivistNationalIdType Parse(object value) => EconomicActivistNationalIdType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, EconomicActivistNationalIdType value) => parameter.Value = value.GetDbValue();
}


