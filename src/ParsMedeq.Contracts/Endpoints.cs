using ParsMedeq.Contracts.UserContracts;
using SRH.PresentationApi.ApiEndpoint;

namespace ParsMedeq.Contracts;

public static class Endpoints
{
    public readonly static UserEndpoint User = new();
    public readonly static ProductEndpoint Product = new();
}
public static class EndpointMetadata
{
    public readonly static ApiEndpointItem Api = new("api", null);
    public readonly static ApiEndpointItem V1 = new("v1", Api);

    public readonly static ApiEndpointItem User = new("user", V1);
    public readonly static ApiEndpointItem Product = new("product", V1);
    public readonly static ApiEndpointItem Admin = new("admin", V1);
}