global using ParsMedeq.Domain.Types.ProfileTypes;

namespace ParsMedeq.Infrastructure.Persistance.DapperValueConverters;
sealed class ProfileLastNameTypeDapperTypeMapper : SqlMapper.TypeHandler<ProfileLastNameType>
{
	public override ProfileLastNameType Parse(object value) => ProfileLastNameType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, ProfileLastNameType value) => parameter.Value = value.GetDbValue();
}


