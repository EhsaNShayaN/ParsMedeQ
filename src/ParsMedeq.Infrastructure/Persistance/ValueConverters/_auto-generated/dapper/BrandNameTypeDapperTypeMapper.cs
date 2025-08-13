global using ParsMedeQ.Domain.Types.BrandTypes;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class BrandNameTypeDapperTypeMapper : SqlMapper.TypeHandler<BrandNameType>
{
	public override BrandNameType Parse(object value) => BrandNameType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, BrandNameType value) => parameter.Value = value.GetDbValue();
}


