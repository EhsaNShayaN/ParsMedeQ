using SRH.Sql.ConfigProvider.Persistance.ValueConverters;
using SRH.Sql.ConfigProvider.Persistance;
using Microsoft.Extensions.Configuration;

namespace SRH.Sql.ConfigProvider;

public static class DbConfigExtensions
{
    public static IConfigurationBuilder AddSRHDbConfig(this ConfigurationManager manager,
        string connectionString,
        string environment,
        SettingApplicationName applicationName,
        SettingVersion version,
        TimeSpan? pollingInterval,
        bool reloadOnChange)
    {
        SqlMapper.AddTypeHandler(new SettingApplicationNameConverter());
        SqlMapper.AddTypeHandler(new SettingVersionConverter());
        SqlMapper.AddTypeHandler(new SettingKeyConverter());

        IConfigurationBuilder configBuilder = manager;
        return configBuilder.Add(new DbConfigSource(connectionString,
            reloadOnChange,
            environment,
            applicationName,
            version,
            pollingInterval ?? TimeSpan.FromMinutes(1)));
    }

    public static IConfigurationBuilder AddSRHDbConfig(this IConfigurationBuilder manager,
       string connectionString,
       string environment,
       SettingApplicationName applicationName,
       SettingVersion version,
       TimeSpan? pollingInterval,
       bool reloadOnChange)
    {
        SqlMapper.AddTypeHandler(new SettingApplicationNameConverter());
        SqlMapper.AddTypeHandler(new SettingVersionConverter());
        SqlMapper.AddTypeHandler(new SettingKeyConverter());

        IConfigurationBuilder configBuilder = manager;
        return configBuilder.Add(new DbConfigSource(connectionString,
            reloadOnChange,
            environment,
            applicationName,
            version,
            pollingInterval ?? TimeSpan.FromMinutes(1)));
    }

}