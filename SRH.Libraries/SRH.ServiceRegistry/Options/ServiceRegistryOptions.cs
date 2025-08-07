namespace SRH.ServiceRegistry.Options;

public sealed class ServiceRegistryOptions
{
    public string ServiceName { get; set; } = string.Empty;
    public string ServiceId { get; set; } = string.Empty;
    public string ServiceAddress { get; set; } = string.Empty;
    public int ServicePort { get; set; }
    public bool HasHealthCheck { get; set; }
    public ServiceHealthcheckOptions? HealthCheck { get; set; }
    public string[] ServiceTags { get; set; } = [];
}
