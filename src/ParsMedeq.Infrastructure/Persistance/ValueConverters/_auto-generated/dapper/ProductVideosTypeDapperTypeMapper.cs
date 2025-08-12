global using ParsMedeq.Domain.Types.ProductTypes;

namespace ParsMedeq.Infrastructure.Persistance.DapperValueConverters;
sealed class ProductVideosTypeDapperTypeMapper : SqlMapper.TypeHandler<ProductVideosType>
{
	public override ProductVideosType Parse(object value) => ProductVideosType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, ProductVideosType value) => parameter.Value = value.GetDbValue();
}


