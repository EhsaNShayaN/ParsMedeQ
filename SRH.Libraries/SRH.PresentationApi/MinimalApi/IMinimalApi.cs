using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace SRH.PresentationApi.MinimalApi;
public interface IMinimalApiEndpoint
{
    RouteHandlerBuilder AddRoute(IEndpointRouteBuilder routeBuilder);
}
