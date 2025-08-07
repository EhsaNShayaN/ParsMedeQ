namespace SRH.Sql.ConfigProvider.Persistance.ValueObjects;

public readonly record struct SettingVersion(
    int Major,
    int Minor,
    int Patch,
    int Revision)
{
    public readonly static SettingVersion AllVersions = new SettingVersion(0, 0, 0, 0);
    public readonly static SettingVersion Version1 = new SettingVersion(1, 0, 0, 0);

    public SettingVersion() : this(0, 0, 0, 0) { }

    public string GetDbValue() => $"{Major}.{Minor}.{Patch}{(Revision > 0 ? $".{Revision.ToString().PadLeft(6, '0')}" : $".0")}";

    public static SettingVersion FromDb(string version)
    {
        var parts = version.Split('.');
        if (parts.Length < 3) return Version1;

        return new SettingVersion(
            Convert.ToInt32(parts[0]),
            Convert.ToInt32(parts[1]),
            Convert.ToInt32(parts[2]),
            Convert.ToInt32(parts[3]));
    }
}
