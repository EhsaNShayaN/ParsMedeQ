global using ParsMedeQ.Domain.Types.VariantTypes;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class VariantIdTypeDapperTypeMapper : SqlMapper.TypeHandler<VariantIdType>
{
	public override VariantIdType Parse(object value) => VariantIdType.FromDb(Convert.ToInt32(value));
	public override void SetValue(IDbDataParameter parameter, VariantIdType value) => parameter.Value = value.GetDbValue();
}


