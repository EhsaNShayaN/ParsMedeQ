global using EShop.Domain.Types.CategoryTypes;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class CategoryIdTypeDapperTypeMapper : SqlMapper.TypeHandler<CategoryIdType>
{
	public override CategoryIdType Parse(object value) => CategoryIdType.FromDb(Convert.ToInt32(value));
	public override void SetValue(IDbDataParameter parameter, CategoryIdType value) => parameter.Value = value.GetDbValue();
}


