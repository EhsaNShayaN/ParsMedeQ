global using EShop.Domain.Types.BrandTypes;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class BrandImageTypeDapperTypeMapper : SqlMapper.TypeHandler<BrandImageType>
{
	public override BrandImageType Parse(object value) => BrandImageType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, BrandImageType value) => parameter.Value = value.GetDbValue();
}


