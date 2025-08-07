namespace SRH.Sql.ConfigProvider.Persistance.Entities;

public sealed class Settings

{
    public int Id { get; set; }
    public SettingApplicationName ApplicationName { get; set; }
    public string Environment { get; set; } = string.Empty;
    public SettingKey Key { get; set; }
    public string Value { get; set; } = string.Empty;
    public SettingVersion Version { get; set; }

}
