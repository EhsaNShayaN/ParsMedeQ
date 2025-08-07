namespace SRH.Sql.ConfigProvider.Persistance;

public sealed class DbConfigReadRepository : GenericPrimitiveReadRepositoryBase<DbConfigSourceContext>, IDisposable
{
    public DbConfigReadRepository(DbConfigSourceContext dbContext) : base(dbContext) { }

    public async ValueTask<Settings[]> GetSettingsAsync(SettingApplicationName appName, SettingVersion ver, string env, CancellationToken cancellationToken)
    {
        var result = new List<Settings>();

        await foreach (var item in this.ExecuteReaderAsync<Settings>(
            DapperCommandDefinitionBuilder
                .StreamedProcedure("Settings_Get")
                .SetParameter("appname", appName.Value)
                .SetParameter("version", ver.GetDbValue())
                .SetParameter("env", env)
                .Build(cancellationToken), cancellationToken))
        {
            if (item is null) continue;
            result.Add(item!);
        }
        return result.ToArray();
    }
    public Settings[] GetSettings(SettingApplicationName appName, SettingVersion ver, string env)
    {
        var result = new List<Settings>();

        foreach (var item in this.ExecuteReader<Settings>(
            DapperCommandDefinitionBuilder
                .StreamedProcedure("Settings_Get")
                .SetParameter("appname", appName.Value)
                .SetParameter("version", ver.GetDbValue())
                .SetParameter("env", env)
                .Build()))
        {
            if (item is null) continue;
            result.Add(item!);
        }
        return result.ToArray();
    }

    public void Dispose() => this.DbContext.Dispose();
}
