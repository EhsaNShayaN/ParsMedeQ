global using ParsMedeq.Domain.Types.BrandTypes;

namespace ParsMedeq.Infrastructure.Persistance.DapperValueConverters;
sealed class BrandTitleTypeDapperTypeMapper : SqlMapper.TypeHandler<BrandTitleType>
{
	public override BrandTitleType Parse(object value) => BrandTitleType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, BrandTitleType value) => parameter.Value = value.GetDbValue();
}


