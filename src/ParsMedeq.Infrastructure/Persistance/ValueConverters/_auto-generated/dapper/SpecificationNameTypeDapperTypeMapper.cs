global using ParsMedeQ.Domain.Types.SpecificationTypes;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class SpecificationNameTypeDapperTypeMapper : SqlMapper.TypeHandler<SpecificationNameType>
{
	public override SpecificationNameType Parse(object value) => SpecificationNameType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, SpecificationNameType value) => parameter.Value = value.GetDbValue();
}


