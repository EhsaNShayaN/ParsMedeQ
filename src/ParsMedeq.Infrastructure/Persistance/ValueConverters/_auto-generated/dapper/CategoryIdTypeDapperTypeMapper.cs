global using ParsMedeq.Domain.Types.CategoryTypes;

namespace ParsMedeq.Infrastructure.Persistance.DapperValueConverters;
sealed class CategoryIdTypeDapperTypeMapper : SqlMapper.TypeHandler<CategoryIdType>
{
	public override CategoryIdType Parse(object value) => CategoryIdType.FromDb(Convert.ToInt32(value));
	public override void SetValue(IDbDataParameter parameter, CategoryIdType value) => parameter.Value = value.GetDbValue();
}


