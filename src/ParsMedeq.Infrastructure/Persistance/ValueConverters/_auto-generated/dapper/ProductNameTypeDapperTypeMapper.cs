global using EShop.Domain.Types.ProductTypes;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class ProductNameTypeDapperTypeMapper : SqlMapper.TypeHandler<ProductNameType>
{
	public override ProductNameType Parse(object value) => ProductNameType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, ProductNameType value) => parameter.Value = value.GetDbValue();
}


