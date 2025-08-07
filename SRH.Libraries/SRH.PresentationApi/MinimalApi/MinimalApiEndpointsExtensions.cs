using SRH.PresentationApi.MinimalApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class MinimalApiEndpointsExtensions
{
    public static IServiceCollection AddMinimalEndpoints(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var minimalEndpoint in assemblies.SelectMany(assembly => assembly.
            DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IMinimalApiEndpoint))))
            .ToArray())
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

