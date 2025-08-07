namespace SRH.Sql.ConfigProvider.Persistance;

public sealed class SettingApplicationNameConverter : SqlMapper.TypeHandler<SettingApplicationName>
{
    public override SettingApplicationName Parse(object value) => SettingApplicationName.FromDb(value?.ToString() ?? string.Empty);
    public override void SetValue(IDbDataParameter parameter, SettingApplicationName value)
    {
        throw new NotImplementedException();
    }
}
