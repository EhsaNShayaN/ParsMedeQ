using Consul;
using SRH.ServiceRegistry.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace SRH.ServiceRegistry;
public static class ConsulServiceRegistrationServiceExtension
{
    public static IServiceCollection InstallServiceRegistrarService(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<ServiceRegistryOptions>(config.GetSection("ServiceRegistry"));
        services.Configure<ConsulOptions>(config.GetSection("Consul"));

        services.TryAddSingleton<ConsulClient>(sp =>
        {
            var consulOptions = sp.GetRequiredService<IOptions<ConsulOptions>>();
            return new ConsulClient(new ConsulClientConfiguration()
            {
                Address = new Uri(consulOptions.Value.Url)
            });
        });

        services.TryAddSingleton<IConsulClient>(sp =>
        {
            var consulOptions = sp.GetRequiredService<IOptions<ConsulOptions>>();
            return new ConsulClient(new ConsulClientConfiguration()
            {
                Address = new Uri(consulOptions.Value.Url)
            });
        });

        services.TryAddSingleton<ConsulServiceRegistrationService>();

        services.AddHostedService<ServiceRegistrarHostedService>();

        return services;
    }
}