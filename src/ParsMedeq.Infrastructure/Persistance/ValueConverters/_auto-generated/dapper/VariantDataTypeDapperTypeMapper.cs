global using ParsMedeQ.Domain.Types.VariantTypes;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class VariantDataTypeDapperTypeMapper : SqlMapper.TypeHandler<VariantDataType>
{
	public override VariantDataType Parse(object value) => VariantDataType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, VariantDataType value) => parameter.Value = value.GetDbValue();
}


