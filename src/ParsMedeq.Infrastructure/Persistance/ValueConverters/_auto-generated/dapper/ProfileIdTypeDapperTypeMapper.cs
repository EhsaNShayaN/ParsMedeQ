global using EShop.Domain.Types.ProfileTypes;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class ProfileIdTypeDapperTypeMapper : SqlMapper.TypeHandler<ProfileIdType>
{
	public override ProfileIdType Parse(object value) => ProfileIdType.FromDb(Convert.ToInt32(value));
	public override void SetValue(IDbDataParameter parameter, ProfileIdType value) => parameter.Value = value.GetDbValue();
}


