namespace SRH.Sql.ConfigProvider.Persistance.ValueObjects;

public readonly record struct SettingKey(string Value)
{
    public readonly static SettingApplicationName AllApplications = new SettingApplicationName("*");

    public SettingKey() : this(string.Empty) { }

    public static SettingKey FromDb(string key)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new InvalidDataException("SettingKey can not be empty");
        return new SettingKey(key/*.Replace(":", "__")*/);
    }
}
