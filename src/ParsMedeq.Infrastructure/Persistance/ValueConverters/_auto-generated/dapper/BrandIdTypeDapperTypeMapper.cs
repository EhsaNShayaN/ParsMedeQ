global using EShop.Domain.Types.BrandTypes;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class BrandIdTypeDapperTypeMapper : SqlMapper.TypeHandler<BrandIdType>
{
	public override BrandIdType Parse(object value) => BrandIdType.FromDb(Convert.ToInt32(value));
	public override void SetValue(IDbDataParameter parameter, BrandIdType value) => parameter.Value = value.GetDbValue();
}


