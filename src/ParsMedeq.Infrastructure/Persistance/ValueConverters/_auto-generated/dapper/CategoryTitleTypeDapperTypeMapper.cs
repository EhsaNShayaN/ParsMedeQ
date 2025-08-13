global using ParsMedeQ.Domain.Types.CategoryTypes;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class CategoryTitleTypeDapperTypeMapper : SqlMapper.TypeHandler<CategoryTitleType>
{
	public override CategoryTitleType Parse(object value) => CategoryTitleType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, CategoryTitleType value) => parameter.Value = value.GetDbValue();
}


