global using ParsMedeq.Domain.Types.ProductTypes;

namespace ParsMedeq.Infrastructure.Persistance.DapperValueConverters;
sealed class ProductIdTypeDapperTypeMapper : SqlMapper.TypeHandler<ProductIdType>
{
	public override ProductIdType Parse(object value) => ProductIdType.FromDb(Convert.ToInt32(value));
	public override void SetValue(IDbDataParameter parameter, ProductIdType value) => parameter.Value = value.GetDbValue();
}


