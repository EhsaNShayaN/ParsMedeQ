using Microsoft.Extensions.Configuration;

namespace SRH.Sql.ConfigProvider;

public sealed class DbConfigSource : IConfigurationSource
{
    #region " Fields "
    private readonly string _connectionString;
    private readonly bool _reloadOnChange;
    private readonly string _environment;
    private readonly SettingApplicationName _applicationName;
    private readonly SettingVersion _version;
    private readonly TimeSpan _pollingInterval;
    #endregion

    #region " Properties "
    public string Environment => _environment;
    public SettingApplicationName ApplicationName => _applicationName;
    public SettingVersion Version => _version;
    public TimeSpan PollingInterval => _pollingInterval;
    public bool ReloadOnChange => _reloadOnChange;
    #endregion

    public DbConfigSource(
        string connectionString,
        bool reloadOnChange,
        string environment,
        SettingApplicationName applicationName,
        SettingVersion version,
        TimeSpan pollingInterval)
    {
        _connectionString = connectionString;
        _reloadOnChange = reloadOnChange;
        _environment = environment;
        _applicationName = applicationName;
        _version = version;
        _pollingInterval = pollingInterval;
    }


    public IConfigurationProvider Build(IConfigurationBuilder builder) => new DbConfigProvider(_connectionString,this);
}
