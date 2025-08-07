using EShop.Application.Services.Signin;
using EShop.Domain.Abstractions;
using EShop.Domain.DomainServices.SigninService;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.FeatureManagement;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging;
using SRH.MediatRMessaging.Behaviours;
using SRH.ServiceInstaller;
using System.Reflection;

namespace EShop.Application.DI;
internal sealed class ProjectServiceInstaller : IServiceInstaller
{
    public Assembly[]? DependantAssemblies => null;

    public IServiceCollection InstallService(IServiceCollection services, IConfiguration config)
    {
        return services
            .InstallDomainEntityValidators(config, this.DependantAssemblies)
            .InstallMediatRServices()
            .InstallRequestCollapser()
            .InstallFeatureManagment()
            .InstallSigninService();
    }
}

static class ServiceCollectionExtension
{
    public static IServiceCollection InstallDomainEntityValidators(this IServiceCollection services,
       IConfiguration config,
       Assembly[]? dependantAssemblies)
    {
        services.Scan(scan =>
            scan
                .FromAssemblies([ApplicationAssemblyReference.Assembly, .. dependantAssemblies ?? []])
                .AddClasses(classes => classes.AssignableTo<IDomainEntityValidator>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        return services;
    }

    public static IServiceCollection InstallMediatRServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), MediatRMessaginAssemblyReference.Assembly);
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehaviour<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehaviour<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            config.AddBehavior(typeof(IRequestExceptionHandler<,,>), typeof(ValidationExceptionHandlerBehaviour<,,>));
        });

        return services;
    }
    public static IServiceCollection InstallRequestCollapser(this IServiceCollection services)
    {
        services.TryAddSingleton<IAsyncRequestCollapserPolicy>(_ => AsyncRequestCollapserPolicy.Create());
        return services;
    }
    public static IServiceCollection InstallFeatureManagment(this IServiceCollection services)
    {
        services.AddFeatureManagement();

        return services;
    }
    public static IServiceCollection InstallSigninService(this IServiceCollection services)
    {
        services.TryAddScoped<ISigninService, SigninService>();

        return services;
    }
}
