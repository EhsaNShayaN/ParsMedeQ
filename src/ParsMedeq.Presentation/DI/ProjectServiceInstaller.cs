using EShop.Presentation.GlobalExceptionHandlers;
using EShop.Presentation.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SRH.ServiceInstaller;
using System.Reflection;

namespace EShop.Presentation.DI;
internal sealed class ProjectServiceInstaller : IServiceInstaller
{
    public Assembly[]? DependantAssemblies => null;

    public IServiceCollection InstallService(IServiceCollection services, IConfiguration config) =>
        services
        .ConfigureOptions<JsonOptionsConfigurator>()
        .ScanAndAddPresentationMappers([PresentationAssemblyReference.Assembly])
        .InstallGlobalExceptionHandler();
}

static class ServiceCollectionExtension
{
    static readonly Type MaperGenericInterfaceType = typeof(IPresentationMapper<,>);

    public static IServiceCollection InstallGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }

    internal static IServiceCollection ScanAndAddPresentationMappers(this IServiceCollection services, params Assembly[] assemblies)
    {
        var implementingTypes = assemblies.SelectMany(a => a.GetTypes())
            .Where(type => type is { IsAbstract: false } && type.GetInterfaces().Any(
                i => i.IsGenericType && i.GetGenericTypeDefinition() == MaperGenericInterfaceType))
            .ToArray();

        foreach (var type in implementingTypes)
        {
            var serviceType = MaperGenericInterfaceType.MakeGenericType(type.GetInterfaces()
                .First(i => i.GetGenericTypeDefinition() == MaperGenericInterfaceType)
                .GetGenericArguments());

            services.Add(
                ServiceDescriptor.Describe(serviceType, type, ServiceLifetime.Transient));
        }
        return services;

    }
}
