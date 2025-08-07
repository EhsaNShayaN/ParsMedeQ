global using EShop.Domain.Types.ProductTypes;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class ProductVideosTypeDapperTypeMapper : SqlMapper.TypeHandler<ProductVideosType>
{
	public override ProductVideosType Parse(object value) => ProductVideosType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, ProductVideosType value) => parameter.Value = value.GetDbValue();
}


