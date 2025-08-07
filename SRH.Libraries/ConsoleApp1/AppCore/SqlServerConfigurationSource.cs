using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace ConsoleApp1.AppCore;
public interface ISqlServerWatcher : IDisposable
{
    IChangeToken Watch();
}
public sealed class SqlServerConfigurationSource : IConfigurationSource
{
    public string ConnectionString { get; set; } = string.Empty;
    public string ServiceCode { get; set; } = string.Empty;
    public string ServiceVersion { get; set; } = string.Empty;
    public ISqlServerWatcher SqlServerWatcher { get; set; }
    public IConfigurationProvider Build(IConfigurationBuilder builder) => new SqlServerConfigurationProvider(this);
}
public sealed class SqlServerConfigurationProvider : ConfigurationProvider
{
    const string _Query = """"
            SELECT 
            	Src.[Key],
            	Src.[Value]
            FROM ServiceSettings AS Src
            INNER JOIN 
            (
            	SELECT 
            		[Key],
            		MAX(ServiceVersion) AS KeyVersion
            	FROM ServiceSettings
            	WHERE 
            		ServiceCode = @serviceCode
            		AND ServiceVersion <= @serviceVersion
            	GROUP BY  [Key]
            ) AS X
            ON Src.[Key] = x.[Key] AND Src.ServiceVersion = x.KeyVersion
            AND src.ServiceCode = @serviceCode;
            """";


    private readonly SqlServerConfigurationSource _source;
    private IDisposable _changeTokenRegistration = null;

    public SqlServerConfigurationProvider(SqlServerConfigurationSource source)
    {
        this._source = source;

        if (_source.SqlServerWatcher is not null)
        {
            _changeTokenRegistration = ChangeToken.OnChange(
                () => _source.SqlServerWatcher.Watch(),
                Load
            );
        }
    }

    public override void Load()
    {
        Console.WriteLine("************ loading configs ****************");
        IDictionary<string, string> dic = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        var connection = new System.Data.SqlClient.SqlConnection(_source.ConnectionString);
        this.Data.Clear();
        var d = connection.Query<(string Key, string Value)>(_Query,
            new Dictionary<string, object>()
            {
                { "serviceCode", this._source.ServiceCode },
                { "serviceVersion", this._source.ServiceVersion }
            }).ToDictionary(
                x => x.Key,
                x => x.Value) ?? new Dictionary<string, string?>(StringComparer.InvariantCultureIgnoreCase);

        this.Data = d;
    }
}
internal class SqlServerPeriodicalWatcher : ISqlServerWatcher
{
    private readonly TimeSpan _refreshInterval;
    private IChangeToken _changeToken;
    private readonly Timer _timer;
    private CancellationTokenSource _cancellationTokenSource;

    public SqlServerPeriodicalWatcher(TimeSpan refreshInterval)
    {
        _refreshInterval = refreshInterval;
        _timer = new Timer(Change, null, TimeSpan.Zero, _refreshInterval);
    }

    private void Change(object state)
    {
        _cancellationTokenSource?.Cancel();
    }

    public IChangeToken Watch()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _changeToken = new CancellationChangeToken(_cancellationTokenSource.Token);

        return _changeToken;
    }

    public void Dispose()
    {
        _timer?.Dispose();
        _cancellationTokenSource?.Dispose();
    }
}
public static class SqlServerConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddSqlServer(this IConfigurationBuilder builder,
        string connectionString,
        string serviceCode,
        string serviceVersion)
    {
        return builder.Add(new SqlServerConfigurationSource
        {
            ConnectionString = connectionString,
            ServiceCode = serviceCode,
            ServiceVersion = serviceVersion,
            SqlServerWatcher = new SqlServerPeriodicalWatcher(TimeSpan.FromSeconds(3))
        });
    }
}
public readonly record struct SqlSettingItem
{
    public readonly string ServiceCode { get; }
    public readonly string ServiceVersion { get; }
    public readonly string Key { get; }
    public readonly string Value { get; }

    public SqlSettingItem(string serviceCode, string serviceVersion, string key, string value)
    {
        this.ServiceCode = serviceCode;
        this.ServiceVersion = serviceVersion;
        this.Key = key;
        this.Value = value;
    }

}
public class EmailServiceOptions
{
    public string ApiKey { get; set; }
}
