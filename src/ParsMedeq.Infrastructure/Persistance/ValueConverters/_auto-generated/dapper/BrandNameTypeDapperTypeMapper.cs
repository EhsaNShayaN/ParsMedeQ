global using EShop.Domain.Types.BrandTypes;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class BrandNameTypeDapperTypeMapper : SqlMapper.TypeHandler<BrandNameType>
{
	public override BrandNameType Parse(object value) => BrandNameType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, BrandNameType value) => parameter.Value = value.GetDbValue();
}


