global using EShop.Domain.Types.CategoryTypes;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class CategoryImageTypeDapperTypeMapper : SqlMapper.TypeHandler<CategoryImageType>
{
	public override CategoryImageType Parse(object value) => CategoryImageType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, CategoryImageType value) => parameter.Value = value.GetDbValue();
}


