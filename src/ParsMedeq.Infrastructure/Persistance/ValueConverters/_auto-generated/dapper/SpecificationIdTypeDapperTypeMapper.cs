global using EShop.Domain.Types.SpecificationTypes;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class SpecificationIdTypeDapperTypeMapper : SqlMapper.TypeHandler<SpecificationIdType>
{
	public override SpecificationIdType Parse(object value) => SpecificationIdType.FromDb(Convert.ToInt32(value));
	public override void SetValue(IDbDataParameter parameter, SpecificationIdType value) => parameter.Value = value.GetDbValue();
}


