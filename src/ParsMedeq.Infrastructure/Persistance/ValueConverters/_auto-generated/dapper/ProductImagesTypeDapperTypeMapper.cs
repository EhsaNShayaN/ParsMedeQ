global using EShop.Domain.Types.ProductTypes;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class ProductImagesTypeDapperTypeMapper : SqlMapper.TypeHandler<ProductImagesType>
{
	public override ProductImagesType Parse(object value) => ProductImagesType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, ProductImagesType value) => parameter.Value = value.GetDbValue();
}


