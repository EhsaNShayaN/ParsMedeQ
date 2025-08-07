namespace SRH.Sql.ConfigProvider.Persistance.ValueConverters;

public sealed class SettingKeyConverter : SqlMapper.TypeHandler<SettingKey>
{
    public override SettingKey Parse(object value) => SettingKey.FromDb(value?.ToString() ?? string.Empty);
    public override void SetValue(IDbDataParameter parameter, SettingKey value)
    {
        throw new NotImplementedException();
    }
}
