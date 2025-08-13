global using ParsMedeQ.Domain.Types.BrandTypes;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class BrandTitleTypeDapperTypeMapper : SqlMapper.TypeHandler<BrandTitleType>
{
	public override BrandTitleType Parse(object value) => BrandTitleType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, BrandTitleType value) => parameter.Value = value.GetDbValue();
}


