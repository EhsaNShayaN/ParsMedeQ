global using ParsMedeQ.Domain.Types.UserId;

namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class UserIdTypeDapperTypeMapper : SqlMapper.TypeHandler<UserIdType>
{
	public override UserIdType Parse(object value) => UserIdType.FromDb(Convert.ToInt32(value));
	public override void SetValue(IDbDataParameter parameter, UserIdType value) => parameter.Value = value.GetDbValue();
}


