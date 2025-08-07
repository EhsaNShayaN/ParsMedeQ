global using EShop.Domain.Types.VariantTypes;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class VariantNameTypeDapperTypeMapper : SqlMapper.TypeHandler<VariantNameType>
{
	public override VariantNameType Parse(object value) => VariantNameType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, VariantNameType value) => parameter.Value = value.GetDbValue();
}


