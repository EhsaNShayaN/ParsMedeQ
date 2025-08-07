using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace SRH.Maply;

public static class MaplyServiceCollectionExtension
{
    public static IServiceCollection ScanAndAddMaplyMappers(this IServiceCollection services, params Assembly[] assemblies)
    {
        // Get all types in the assembly that implement the generic interface

        Maply.Defualt.ScanAndAddMappers(services, assemblies);

        services.TryAddTransient<IMapper>(sp => Maply.Defualt);

        return services;
    }

}
