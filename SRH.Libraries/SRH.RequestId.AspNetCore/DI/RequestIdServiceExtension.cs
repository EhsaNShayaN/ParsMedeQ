using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SRH.RequestId.AspNetCore.DI;

public static class RequestIdServiceExtension
{
    public static IRequestIdBuilder AddRequestId(this IServiceCollection services)
    {
        services.TryAddSingleton<IRequestIdContextAccessor, RequestIdContextAccessor>();
        services.TryAddSingleton<IRequestIdContextFactrory, RequestIdContextFactrory>();

        return new RequestIdServiceBuilder(services);
    }

    public static IRequestIdBuilder AddRequestId<T>(this IServiceCollection services) where T : class, IRequestIdProvider
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (services.Any(x => x.ServiceType == typeof(IRequestIdProvider)))
        {
            throw new InvalidOperationException("A provider has already been added.");
        }

        var builder = AddRequestId(services);

        builder.Services.TryAddSingleton<IRequestIdProvider, T>();

        return builder;
    }

    public static IRequestIdBuilder AddRequestId(this IServiceCollection services, Action<RequestIdOptions> configure)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        services.Configure(configure);

        return services.AddRequestId();
    }

    public static IRequestIdBuilder AddRequestId<T>(this IServiceCollection services, Action<RequestIdOptions> configure) where T : class, IRequestIdProvider
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (services.Any(x => x.ServiceType == typeof(IRequestIdProvider)))
        {
            throw new InvalidOperationException("A provider has already been added.");
        }
        
        services.Configure(configure);

        return services.AddRequestId<T>();
    }

    public static IRequestIdBuilder AddDefaultRequestId(this IServiceCollection services) => services.AddRequestId().WithGuidProvider();
    
    public static IRequestIdBuilder AddDefaultRequestId(this IServiceCollection services, Action<RequestIdOptions> configure) => services.AddRequestId(configure).WithGuidProvider();
}
