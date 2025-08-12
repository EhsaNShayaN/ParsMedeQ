global using ParsMedeq.Domain.Types.ProfileTypes;

namespace ParsMedeq.Infrastructure.Persistance.DapperValueConverters;
sealed class ProfileMobileTypeDapperTypeMapper : SqlMapper.TypeHandler<ProfileMobileType>
{
	public override ProfileMobileType Parse(object value) => ProfileMobileType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, ProfileMobileType value) => parameter.Value = value.GetDbValue();
}


