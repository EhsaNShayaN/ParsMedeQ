using SRH.Sql.ConfigProvider.Persistance;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace SRH.Sql.ConfigProvider;
public sealed class DbConfigProvider : ConfigurationProvider, IDisposable
{
    #region " Fields "
    private bool _disposed;
    private readonly string _connectionString;
    private readonly DbConfigSource _configurationSource;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private byte[] _lastComputedHash = [];
    private Task? _watchDbTask = null;
    #endregion

    public DbConfigProvider(
        string connectionString,
        DbConfigSource configurationSource)
    {
        _connectionString = connectionString;
        _configurationSource = configurationSource;
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public override void Load()
    {
        try
        {
            if (_watchDbTask is not null) return;

            var data = GetData()!;
            _lastComputedHash = ComputeHash(data!);
            this.Data = BuildSettingsDictionary(data!);
            var ct = _cancellationTokenSource.Token;
            if (_configurationSource.ReloadOnChange)
            {
                _watchDbTask = Task.Run(() => WatchDatabase(ct), ct);
            }
        }
        catch (Exception ex)
        {
            //this._logger.LogCritical(ex, "Can not load settings");
            throw;
        }
    }

    private IDictionary<string, string> GetData()
    {
        using var dbContext = new DbConfigSourceContext(_connectionString);
        var repo = new DbConfigReadRepository(dbContext);
        var settings = repo.GetSettings(
            _configurationSource.ApplicationName,
            _configurationSource.Version,
            _configurationSource.Environment);

        return settings?.ToDictionary(
                s => s.Key.Value,
                s => s.Value, StringComparer.OrdinalIgnoreCase
            ) ?? new(StringComparer.OrdinalIgnoreCase);
    }
    private async Task<IDictionary<string, string>> GetDataAsync(CancellationToken cancellationToken)
    {
        using var dbContext = new DbConfigSourceContext(_connectionString);
        var repo = new DbConfigReadRepository(dbContext);
        var settings = await repo.GetSettingsAsync(
            _configurationSource.ApplicationName,
            _configurationSource.Version,
            _configurationSource.Environment, cancellationToken)
            .ConfigureAwait(false);

        return settings?.ToDictionary(
                s => s.Key.Value,
                s => s.Value, StringComparer.OrdinalIgnoreCase
            ) ?? new(StringComparer.OrdinalIgnoreCase);
    }
    private byte[] ComputeHash(IDictionary<string, string> dict)
    {
        List<byte> byteDict = new List<byte>();
        foreach (KeyValuePair<string, string> item in dict)
        {
            byteDict.AddRange(Encoding.Unicode.GetBytes($"{item.Key}{item.Value}"));
        }

        return System.Security.Cryptography.SHA1.Create().ComputeHash(byteDict.ToArray());
    }
    private async Task WatchDatabase(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(_configurationSource.PollingInterval);
            var data = await GetDataAsync(cancellationToken).ConfigureAwait(false);
            var hash = ComputeHash(data!);
            if (!hash.SequenceEqual(_lastComputedHash))
            {
                _lastComputedHash = hash;
                System.Diagnostics.Debug.WriteLine("Some changes affected in settings");
                this.Data = BuildSettingsDictionary(data!);
                this.OnReload();
            }
            else
            {
                //this._logger.LogInformation("there is no changes in settings");
            }
        }
    }
    private IDictionary<string, string?> BuildSettingsDictionary(IDictionary<string, string?> settingsDictionary)
    {
        return settingsDictionary;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("{");
        foreach (KeyValuePair<string, string> setting in settingsDictionary)
        {
            if (setting.Value.StartsWith("{") || setting.Value.StartsWith("["))
                stringBuilder.Append($"\"{setting.Key}\": {setting.Value}, ");
            else
                stringBuilder.Append($"\"{setting.Key}\": \"{setting.Value}\", ");
        }
        stringBuilder.Append("}");

        return JsonConfigurationParser.Parse(stringBuilder.ToString());
    }
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
        _disposed = true;
    }

}
