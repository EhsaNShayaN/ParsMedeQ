global using EShop.Domain.Types.ProductSpecificationTypes;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class ProductSpecificationIdTypeDapperTypeMapper : SqlMapper.TypeHandler<ProductSpecificationIdType>
{
	public override ProductSpecificationIdType Parse(object value) => ProductSpecificationIdType.FromDb(Convert.ToInt32(value));
	public override void SetValue(IDbDataParameter parameter, ProductSpecificationIdType value) => parameter.Value = value.GetDbValue();
}


