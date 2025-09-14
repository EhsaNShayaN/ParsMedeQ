using ParsMedeQ.Contracts.ProductContracts;
using ParsMedeQ.Contracts.ResourceContracts;
using ParsMedeQ.Contracts.UserContracts;
using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeQ.Contracts;

public static class Endpoints
{
    public readonly static UserEndpoint User = new();
    public readonly static ResourceEndpoint Resource = new();
    public readonly static ProductEndpoint Product = new();
}
public static class EndpointMetadata
{
    public readonly static ApiEndpointItem Api = new("api", null);
    public readonly static ApiEndpointItem V1 = new("v1", Api);

    public readonly static ApiEndpointItem User = new("user", V1);
    public readonly static ApiEndpointItem Resource = new("resource", V1);
    public readonly static ApiEndpointItem Product = new("product", V1);
    public readonly static ApiEndpointItem Admin = new("admin", V1);
}