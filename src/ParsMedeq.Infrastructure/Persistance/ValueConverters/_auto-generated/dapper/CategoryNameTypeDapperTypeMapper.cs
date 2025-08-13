global using ParsMedeQ.Domain.Types.CategoryTypes;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class CategoryNameTypeDapperTypeMapper : SqlMapper.TypeHandler<CategoryNameType>
{
	public override CategoryNameType Parse(object value) => CategoryNameType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, CategoryNameType value) => parameter.Value = value.GetDbValue();
}


