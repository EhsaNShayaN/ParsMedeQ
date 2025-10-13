using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SRH.PresentationApi.MinimalApi;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class MinimalApiEndpointsExtensions
{
    public static IServiceCollection AddMinimalEndpoints(this IServiceCollection services, params Assembly[] assemblies)
    {
        var minimalApiEndpoints = assemblies.SelectMany(assembly => assembly.
            DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IMinimalApiEndpoint))))
            .ToArray();

        foreach (var minimalEndpoint in minimalApiEndpoints)
        {
            services.TryAddEnumerable(ServiceDescriptor.Describe(typeof(IMinimalApiEndpoint), minimalEndpoint, ServiceLifetime.Transient));
        }
        return services;
    }

    public static WebApplication MapMinimalEndpoits(this WebApplication app)
    {
        var minimalEndpoits = app.Services.GetRequiredService<IEnumerable<IMinimalApiEndpoint>>();

        foreach (var minimalEndpoint in minimalEndpoits)
        {
            minimalEndpoint.AddRoute(app);
        }

        return app;
    }
}

