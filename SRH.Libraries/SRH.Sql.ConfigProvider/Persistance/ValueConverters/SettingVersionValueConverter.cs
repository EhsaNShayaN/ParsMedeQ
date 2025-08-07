namespace SRH.Sql.ConfigProvider.Persistance.ValueConverters;

sealed class SettingVersionValueConverter : ValueConverter<SettingVersion, string>
{
    public SettingVersionValueConverter() : base(
        src => src.GetDbValue(),
        value => SettingVersion.FromDb(value))
    { }
}
