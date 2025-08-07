using Consul;
using SRH.ServiceRegistry.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SRH.ServiceRegistry;

public sealed class ConsulServiceRegistrationService
{
    private readonly IConfiguration _config;
    private readonly IOptions<ServiceRegistryOptions> _opts;
    private readonly ConsulClient _consulClient;
    private readonly ILogger<ConsulServiceRegistrationService> _logger;
    private readonly string _serviceId = string.Empty;

    public ConsulServiceRegistrationService(
        IConfiguration config,
        IOptions<ServiceRegistryOptions> opts,
        ConsulClient consulClient,
        ILogger<ConsulServiceRegistrationService> logger)
    {
        this._config = config;
        this._opts = opts;
        this._consulClient = consulClient;
        this._logger = logger;

        this._serviceId = $"{this._opts.Value.ServiceName.Replace(" ", "_")}_{Guid.NewGuid().ToString("N")}";
    }

    public async Task RegisterServiceAsync(CancellationToken cancellationToken)
    {
        this._logger.LogInformation("Registering service '{servicename}' on '{serviceaddress}:{serviceport}' with id '{serviceid}'",
            this._opts.Value.ServiceName,
            this._opts.Value.ServiceAddress,
            this._opts.Value.ServicePort,
            this._serviceId);

        var registration = new AgentServiceRegistration
        {
            ID = this._serviceId,
            Name = this._opts.Value.ServiceName,
            Address = this._opts.Value.ServiceAddress,
            Port = this.GetPort(),
            Tags = this._opts.Value.ServiceTags ?? []
        };


        if (
            (this._opts.Value.HealthCheck is ServiceHealthcheckOptions o && (!string.IsNullOrWhiteSpace(o.Url) || !string.IsNullOrWhiteSpace(o.Endpoint)))
            || this._opts.Value.HasHealthCheck)
        {
            var healthCheck = this._opts.Value.HealthCheck ?? ServiceHealthcheckOptions.DefaultServiceHealthcheckOptions;

            var url = healthCheck.Url;
            if (string.IsNullOrWhiteSpace(url))
            {
                var endpoint = healthCheck.Endpoint;
                if (string.IsNullOrWhiteSpace(endpoint))
                {
                    endpoint = "health";
                }
                url = $"{this._opts.Value.ServiceAddress}:{this.GetPort()}/{endpoint}";
            }
            registration.Check = new AgentServiceCheck()
            {
                HTTP = url,
                Interval = healthCheck.Interval,
                Timeout = healthCheck.Timeout,
                DeregisterCriticalServiceAfter = healthCheck.DeregisterCriticalServiceAfter,
#if DEBUG
                TLSSkipVerify = true
#endif
            };
        }

        try
        {
            await this._consulClient.Agent.ServiceRegister(registration, cancellationToken);
        }
        catch (Exception ex)
        {
            this._logger.LogCritical(ex, "Can not register service to service registry");
        }
    }

    public async Task DeregisterServiceAsync(CancellationToken cancellationToken)
    {
        this._logger.LogInformation("Deregistering service '{servicename}' on '{serviceaddress}:{serviceport}' with id '{serviceid}'",
            this._opts.Value.ServiceName,
            this._opts.Value.ServiceAddress,
            this.GetPort(),
            this._serviceId);

        try
        {
            await this._consulClient.Agent.ServiceDeregister(this._serviceId, cancellationToken);
        }
        catch (Exception ex)
        {
            this._logger.LogCritical(ex, "Can not deregister service from service registry");
        }
    }

    public async Task RegisterMe(CancellationToken cancellationToken)
    {
        await this.DeregisterServiceAsync(cancellationToken).ConfigureAwait(false);
        await this.RegisterServiceAsync(cancellationToken).ConfigureAwait(false);

    }

    public int GetPort()
    {
        var cliPortNumber = this._config.GetValue<int?>("port");
        return cliPortNumber ?? this._opts.Value.ServicePort;
    }
}
