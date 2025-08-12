global using ParsMedeq.Domain.Types.SpecificationTypes;

namespace ParsMedeq.Infrastructure.Persistance.DapperValueConverters;
sealed class SpecificationUnitTypeDapperTypeMapper : SqlMapper.TypeHandler<SpecificationUnitType>
{
	public override SpecificationUnitType Parse(object value) => SpecificationUnitType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, SpecificationUnitType value) => parameter.Value = value.GetDbValue();
}


