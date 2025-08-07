using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SRH.ServiceRegistry;

public sealed class ServiceRegistrarHostedService : IHostedService
{
    private readonly ConsulServiceRegistrationService _consulServiceRegistrationService;
    private readonly ILogger<ServiceRegistrarHostedService> _logger;

    public ServiceRegistrarHostedService(
        ConsulServiceRegistrationService consulServiceRegistrationService,
        ILogger<ServiceRegistrarHostedService> logger)
    {
        this._consulServiceRegistrationService = consulServiceRegistrationService;
        this._logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await this._consulServiceRegistrationService.RegisterMe(cancellationToken);
    }
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await this._consulServiceRegistrationService.DeregisterServiceAsync(cancellationToken);
    }
}
