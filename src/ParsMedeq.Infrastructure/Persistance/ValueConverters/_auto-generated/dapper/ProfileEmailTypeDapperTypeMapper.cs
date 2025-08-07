global using EShop.Domain.Types.ProfileTypes;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class ProfileEmailTypeDapperTypeMapper : SqlMapper.TypeHandler<ProfileEmailType>
{
	public override ProfileEmailType Parse(object value) => ProfileEmailType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, ProfileEmailType value) => parameter.Value = value.GetDbValue();
}


