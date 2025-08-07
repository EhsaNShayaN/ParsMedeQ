using SRH.CacheProvider;
using Medallion.Threading.Redis;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;
using System.Text.Json.Serialization;

namespace Microsoft.Extensions.DependencyInjection;

public static class CacheProviderInstaller
{
    public static IServiceCollection InstallDefaultSRHCacheProvider(this IServiceCollection services,
        string redisConnectionstring,
        string applicationName,
        Action<FusionCacheEntryOptions> action)
    {
        services.AddKeyedSingleton<IDistributedLockProvider>(DefaultCacheProvider.ServiceKey, (sp, obj) =>
        {
            var connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionstring);
            return new RedisDistributedSynchronizationProvider(connectionMultiplexer.GetDatabase());
        });

        services.AddMemoryCache();
        services.AddFusionCacheSystemTextJsonSerializer(new System.Text.Json.JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        });
        services.AddStackExchangeRedisCache(opt => opt.Configuration = redisConnectionstring);
        services.AddFusionCacheStackExchangeRedisBackplane(opt => opt.Configuration = redisConnectionstring);

        AddApplicationCacheServices(services, applicationName, action);

        AddDefaultSRHCacheProvider(services);

        return services;
    }

    static IServiceCollection AddApplicationCacheServices(IServiceCollection services,
        string applicationName,
        Action<FusionCacheEntryOptions> action)
    {
        services
           .AddFusionCache("ApplicationCache")
           .WithDefaultEntryOptions(action)
           .WithCacheKeyPrefix($"{applicationName}:ApplicationCache:")
           .WithRegisteredSerializer()
           .WithRegisteredDistributedCache()
           .WithRegisteredBackplane()
           .WithOptions(options =>
           {
               options.DistributedCacheKeyModifierMode = CacheKeyModifierMode.None;
           });

        services
            .AddFusionCache("CachedKeys")
            .WithDefaultEntryOptions(opt =>
            {
                opt.Duration = TimeSpan.MaxValue;
            })
            .WithCacheKeyPrefix($"{applicationName}:CachedKeys:")
            .WithRegisteredSerializer()
            .WithRegisteredDistributedCache()
            .WithRegisteredBackplane()
            .WithOptions(options =>
            {
                options.DistributedCacheKeyModifierMode = CacheKeyModifierMode.None;
            });

        return services;
    }

    static IServiceCollection AddDefaultSRHCacheProvider(IServiceCollection services)
    {
        services.TryAddSingleton<ICacheProvider, DefaultCacheProvider>();

        return services;
    }

}
