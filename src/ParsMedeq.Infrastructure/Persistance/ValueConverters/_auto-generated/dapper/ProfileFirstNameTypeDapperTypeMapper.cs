global using EShop.Domain.Types.ProfileTypes;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class ProfileFirstNameTypeDapperTypeMapper : SqlMapper.TypeHandler<ProfileFirstNameType>
{
	public override ProfileFirstNameType Parse(object value) => ProfileFirstNameType.FromDb(Convert.ToString(value));
	public override void SetValue(IDbDataParameter parameter, ProfileFirstNameType value) => parameter.Value = value.GetDbValue();
}


