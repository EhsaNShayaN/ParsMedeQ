using SRH.Persistance.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
namespace ConsoleApp1.AppCore;

public sealed class ServiceWorker : BackgroundService
{
    private readonly IOptionsMonitor<EmailServiceOptions> _options;
    private readonly SampleDbContext _sampleDbContext;

    public ServiceWorker(IOptionsMonitor<EmailServiceOptions> options, SampleDbContext sampleDbContext)
    {
        this._options = options;
        this._sampleDbContext = sampleDbContext;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var r = this._sampleDbContext.GetSchemaAndTableName<User>();

        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine(this._options.CurrentValue.ApiKey);
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}