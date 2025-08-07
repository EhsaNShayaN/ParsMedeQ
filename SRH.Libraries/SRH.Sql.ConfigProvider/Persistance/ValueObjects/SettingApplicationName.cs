namespace SRH.Sql.ConfigProvider.Persistance.ValueObjects;

public readonly record struct SettingApplicationName(string Value)
{
    public readonly static SettingApplicationName AllApplications = new SettingApplicationName("*");

    public SettingApplicationName() : this(string.Empty) { }
    public static SettingApplicationName FromDb(string appName)
    {
        if (string.IsNullOrWhiteSpace(appName)) return AllApplications;
        return new SettingApplicationName(appName.Trim());
    }
}
