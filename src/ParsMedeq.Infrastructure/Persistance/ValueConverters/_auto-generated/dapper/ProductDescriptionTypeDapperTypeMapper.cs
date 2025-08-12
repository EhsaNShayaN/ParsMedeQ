global using ParsMedeq.Domain.Types.ProductTypes;

namespace ParsMedeq.Infrastructure.Persistance.DapperValueConverters;
sealed class ProductDescriptionTypeDapperTypeMapper : SqlMapper.TypeHandler<ProductDescriptionType>
{
	public override ProductDescriptionType Parse(object value) => ProductDescriptionType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, ProductDescriptionType value) => parameter.Value = value.GetDbValue();
}


