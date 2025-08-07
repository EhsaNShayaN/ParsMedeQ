namespace SRH.Sql.ConfigProvider.Persistance.ValueConverters;

public sealed class SettingVersionConverter : SqlMapper.TypeHandler<SettingVersion>
{
    public override SettingVersion Parse(object value) => SettingVersion.FromDb(value?.ToString() ?? string.Empty);
    public override void SetValue(IDbDataParameter parameter, SettingVersion value)
    {
        throw new NotImplementedException();
    }
}
