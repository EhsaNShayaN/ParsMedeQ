namespace SRH.ServiceRegistry.Options;

public sealed class ServiceHealthcheckOptions
{
    public string Url { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public TimeSpan Interval { get; set; }
    public TimeSpan Timeout { get; set; }
    public TimeSpan DeregisterCriticalServiceAfter { get; set; }

    public ServiceHealthcheckOptions()
    {
        Interval = TimeSpan.FromSeconds(10);
        Timeout = TimeSpan.FromSeconds(1);
        DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1);
    }

    public readonly static ServiceHealthcheckOptions DefaultServiceHealthcheckOptions =
        new ServiceHealthcheckOptions()
        {
            Endpoint = "health",
        };
}
