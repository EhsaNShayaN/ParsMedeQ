global using ParsMedeQ.Domain.Types.ProductVariantTypes;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class ProductVariantIdTypeDapperTypeMapper : SqlMapper.TypeHandler<ProductVariantIdType>
{
	public override ProductVariantIdType Parse(object value) => ProductVariantIdType.FromDb(Convert.ToInt32(value));
	public override void SetValue(IDbDataParameter parameter, ProductVariantIdType value) => parameter.Value = value.GetDbValue();
}


