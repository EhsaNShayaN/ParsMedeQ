global using EShop.Domain.Types.ProductTypes;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class ProductTitleTypeDapperTypeMapper : SqlMapper.TypeHandler<ProductTitleType>
{
	public override ProductTitleType Parse(object value) => ProductTitleType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, ProductTitleType value) => parameter.Value = value.GetDbValue();
}


