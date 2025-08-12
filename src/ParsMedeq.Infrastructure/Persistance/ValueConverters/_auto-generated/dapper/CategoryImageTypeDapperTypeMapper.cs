global using ParsMedeq.Domain.Types.CategoryTypes;

namespace ParsMedeq.Infrastructure.Persistance.DapperValueConverters;
sealed class CategoryImageTypeDapperTypeMapper : SqlMapper.TypeHandler<CategoryImageType>
{
	public override CategoryImageType Parse(object value) => CategoryImageType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, CategoryImageType value) => parameter.Value = value.GetDbValue();
}


