global using ParsMedeQ.Domain.Types.VariantTypes;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class VariantTypeIdTypeDapperTypeMapper : SqlMapper.TypeHandler<VariantTypeIdType>
{
	public override VariantTypeIdType Parse(object value) => VariantTypeIdType.FromId(Convert.ToByte(value));
	public override void SetValue(IDbDataParameter parameter, VariantTypeIdType value) => parameter.Value = value.GetDbValue();
}


