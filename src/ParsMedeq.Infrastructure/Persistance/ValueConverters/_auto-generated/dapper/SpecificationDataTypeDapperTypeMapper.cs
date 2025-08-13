global using ParsMedeQ.Domain.Types.SpecificationTypes;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class SpecificationDataTypeDapperTypeMapper : SqlMapper.TypeHandler<SpecificationDataType>
{
	public override SpecificationDataType Parse(object value) => SpecificationDataType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, SpecificationDataType value) => parameter.Value = value.GetDbValue();
}


