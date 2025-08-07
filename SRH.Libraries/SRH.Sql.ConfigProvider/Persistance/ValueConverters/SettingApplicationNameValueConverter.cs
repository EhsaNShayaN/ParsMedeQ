namespace SRH.Sql.ConfigProvider.Persistance.ValueConverters;

sealed class SettingApplicationNameValueConverter : ValueConverter<SettingApplicationName, string>
{
    public SettingApplicationNameValueConverter() : base(
        src => src.Value,
        value => SettingApplicationName.FromDb(value))
    { }
}
