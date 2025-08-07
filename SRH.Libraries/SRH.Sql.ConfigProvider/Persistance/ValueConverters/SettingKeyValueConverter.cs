namespace SRH.Sql.ConfigProvider.Persistance.ValueConverters;

sealed class SettingKeyValueConverter : ValueConverter<SettingKey, string>
{
    public SettingKeyValueConverter() : base(
        src => src.Value,
        value => SettingKey.FromDb(value))
    { }
}
