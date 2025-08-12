global using ParsMedeq.Domain.Types.BrandTypes;

namespace ParsMedeq.Infrastructure.Persistance.DapperValueConverters;
sealed class BrandNameTypeDapperTypeMapper : SqlMapper.TypeHandler<BrandNameType>
{
	public override BrandNameType Parse(object value) => BrandNameType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, BrandNameType value) => parameter.Value = value.GetDbValue();
}


