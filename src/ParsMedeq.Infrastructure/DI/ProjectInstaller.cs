using Dapper;
using ParsMedeQ.Application;
using ParsMedeQ.Application.Persistance;
using ParsMedeQ.Application.Services.EmailSenderService;
using ParsMedeQ.Application.Services.OTP;
using ParsMedeQ.Application.Services.SmsSenderService;
using ParsMedeQ.Domain.Persistance;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using ParsMedeQ.Infrastructure.Services.EmailSenderService;
using ParsMedeQ.Infrastructure.Services.OTP;
using ParsMedeQ.Infrastructure.Services.SmsSenderService;
using Medallion.Threading;
using Medallion.Threading.SqlServer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http.Resilience;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Polly;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.NewtonsoftJson;

namespace ParsMedeQ.Infrastructure.DI;
internal sealed class ProjectInstaller : IServiceInstaller
{
    public Assembly[]? DependantAssemblies => [ApplicationAssemblyReference.Assembly];

    public IServiceCollection InstallService(IServiceCollection services, IConfiguration config) =>
        services
            .InstallDomainServices(this.DependantAssemblies)
            .InstallIHttpContextAccessor()
            .InstallPersistance(config, this.DependantAssemblies)
            .InstallEmailSenderService(config)
            .InstallSmsSenderService(config)
            .InstallDistributedLockService(config)
            .InstallDpiCacheProvider(config)
            .InstallFusionCache()
            .InstallOTPService()
            .InstallHealthChecks(config)
            .InstallOpenTelemtryTracingAndMetrics(config);
}
static class ServiceCollectionExtension
{
    const string Default_Db_ConfigName = "Default";
    const string Db_ConfigName = "ParsMedeQ";

    internal static IServiceCollection InstallDomainServices(this IServiceCollection services, Assembly[]? dependantAssemblies)
    {
        return services.Scan(scan =>
            scan
                .FromAssemblies([InfrastructureAssemblyReference.Assembly, .. dependantAssemblies ?? []])
                .AddClasses(classes => classes.AssignableTo<Domain.Abstractions.IDomainValidator>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );
    }

    internal static IServiceCollection InstallIHttpContextAccessor(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        return services;
    }

    internal static IServiceCollection InstallPersistance(this IServiceCollection services,
        IConfiguration config,
        Assembly[]? dependantAssemblies)
    {
        services.AddReadWriteDbContext<ReadDbContext, WriteDbContext>(config, Db_ConfigName);

        // Register all repositories
        services.Scan(scan =>
            scan
                .FromAssemblies([InfrastructureAssemblyReference.Assembly, .. dependantAssemblies ?? []])
                .AddClasses(classes => classes.AssignableTo<IDomainRepository>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        // Register all unitofworks
        services.Scan(scan =>
           scan
               .FromAssemblies([InfrastructureAssemblyReference.Assembly, .. dependantAssemblies ?? []])
               .AddClasses(classes => classes.AssignableTo<IUnitOfWork>())
               .AsImplementedInterfaces()
               .WithScopedLifetime()
        );

        AddAllDapperTypeHandlers();

        return services;
    }

    internal static IServiceCollection InstallEmailSenderService(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddFluentEmail("youremail@gmail.com")
            .AddSmtpSender(new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("roozbeh.hoseiny@gmail.com", "coyqmsoztqlwjgck"),
                EnableSsl = true,
            });

        services.TryAddScoped<IEmailSenderService, EmailSenderService>();
        services.Decorate<IEmailSenderService, MoqEmailSenderService>();

        return services;
    }
    internal static IServiceCollection InstallSmsSenderService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmsOptions>(configuration.GetSection("Sms"));
        services.TryAddTransient<SmsLoggerDelegatingHandler>();
        services.TryAddTransient<SmsApiKeyDelegatingHandler>();
        services.AddHttpClient(SmsSenderService.HttpClientName,
            client =>
            {
                client.BaseAddress = new Uri(configuration.GetSection("Sms").Get<SmsOptions>()!.BaseUrl);
            })
            .AddHttpMessageHandler<SmsLoggerDelegatingHandler>()
            .AddHttpMessageHandler<SmsApiKeyDelegatingHandler>()
            .AddResilienceHandler("SMSResilienceStrategy", resilienceBuilder =>
            {
                // Retry Strategy configuration
                resilienceBuilder.AddRetry(new HttpRetryStrategyOptions // Configures retry behavior
                {
                    MaxRetryAttempts = 4, // Maximum retries before throwing an exception (default: 3)

                    Delay = TimeSpan.FromSeconds(2), // Delay between retries (default: varies by strategy)

                    BackoffType = Polly.DelayBackoffType.Exponential, // Exponential backoff for increasing delays (default)

                    UseJitter = true, // Adds random jitter to delay for better distribution (default: false)

                    ShouldHandle = new PredicateBuilder<HttpResponseMessage>() // Defines exceptions to trigger retries
                    .Handle<HttpRequestException>() // Includes any HttpRequestException
                    //.Handle<TimeoutRejectedException>() // Includes any HttpRequestException
                    .HandleResult(response => !response.IsSuccessStatusCode) // Includes non-successful responses
                });

                // Timeout Strategy configuration
                resilienceBuilder.AddTimeout(TimeSpan.FromSeconds(5));

                // Circuit Breaker Strategy configuration
                resilienceBuilder.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions // Configures circuit breaker behavior
                {
                    // Tracks failures within this time frame
                    SamplingDuration = TimeSpan.FromSeconds(10),

                    // Trips the circuit if failure ratio exceeds this within sampling duration (20% failures allowed)
                    FailureRatio = 0.2,

                    // Requires at least this many successful requests within sampling duration to reset
                    MinimumThroughput = 3,

                    // How long the circuit stays open after tripping
                    BreakDuration = TimeSpan.FromSeconds(1),

                    // Defines exceptions to trip the circuit breaker
                    ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                    .Handle<HttpRequestException>() // Includes any HttpRequestException
                    .HandleResult(response => !response.IsSuccessStatusCode) // Includes non-successful responses
                });
            });


        services.TryAddScoped<ISmsSenderService, SmsSenderService>();
        services.Decorate<ISmsSenderService, MoqSmsSenderService>();

        return services;
    }

    internal static IServiceCollection InstallDistributedLockService(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IDistributedLockProvider>(_ =>
            new SqlDistributedSynchronizationProvider(
                GetWriteConnectionstring(config, Db_ConfigName)));

        return services;
    }

    public static IServiceCollection InstallFusionCache(this IServiceCollection services)
    {
        //services.AddMemoryCache();

        services.AddFusionCache()
            .WithDefaultEntryOptions(new FusionCacheEntryOptions
            {
                // CACHE DURATION
                Duration = TimeSpan.FromMinutes(1),

                // FAIL-SAFE OPTIONS
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromHours(2),
                FailSafeThrottleDuration = TimeSpan.FromSeconds(30),

                // FACTORY TIMEOUTS
                FactorySoftTimeout = TimeSpan.FromMilliseconds(100),
                FactoryHardTimeout = TimeSpan.FromMilliseconds(1500),

                DistributedCacheDuration = TimeSpan.FromMinutes(2),
                ReThrowDistributedCacheExceptions = true,
                SkipDistributedCache = false,

            })
            .WithSerializer(new FusionCacheNewtonsoftJsonSerializer())
            .WithDistributedCache(
                //TODO: Use Config
                new RedisCache(new RedisCacheOptions() { Configuration = "127.0.0.1:6379", InstanceName = "ParsMedeQ:" })
            );

        return services;

    }

    public static IServiceCollection InstallOTPService(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.TryAddScoped<IOtpService, OtpService>();

        return services;
    }

    internal static IServiceCollection InstallHealthChecks(this IServiceCollection services, IConfiguration config)
    {
        services.AddHealthChecks()
            .AddSqlServer(sp =>
            {
                var connectionstring = GetConnectionString(config, Db_ConfigName, ApplicationIntent.ReadOnly);
                return connectionstring?.ConnectionString ?? string.Empty;
            },
            healthQuery: "SELECT GetDate();",
            name: "ParsMedeQ Database");

        return services;
    }

    internal static IServiceCollection InstallOpenTelemtryTracingAndMetrics(this IServiceCollection services, IConfiguration config)
    {
        services.AddOpenTelemetry()
            .WithTracing(tracingBuilder =>
            {
                tracingBuilder
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService((config.GetSection("OTLP:ServiceName").Get<string>() ?? "ParsMedeQ(Api)")!))
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddGrpcClientInstrumentation()
                    .AddGrpcCoreInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation(opts =>
                    {
                        opts.SetDbStatementForText = true;
                        opts.SetDbStatementForStoredProcedure = true;
                    })
                    .AddSource("ApplicationHandler")
                    .AddSource("RequestResponseTracer")
                    .AddSource("ExceptionHandler")
                    .SetSampler(new AlwaysOnSampler())
                    .AddOtlpExporter(exporterOptions =>
                    {
                        exporterOptions.Endpoint = new Uri(config.GetSection("OTLP:ExporterUrl").Get<string>() ?? "http://localhost:4317"!);
                    })
                    ;
            });

        return services;
    }

    internal static IServiceCollection InstallDpiCacheProvider(this IServiceCollection services, IConfiguration config)
    {
        var redisConnectionstring = config.GetSection("DistributedCache:ConnectionString").Get<string>();
        var cacheAppName = config.GetSection("DistributedCache:ApplicationName").Get<string>();

        /*
        if (string.IsNullOrWhiteSpace(redisConnectionstring)) throw new Exception("'DistributedCache:ConnectionString' is empty.");
        if (string.IsNullOrWhiteSpace(cacheAppName)) throw new Exception("'DistributedCache:ApplicationName' is empty.");
        */

        services.InstallDefaultSRHCacheProvider(
            redisConnectionstring,
            cacheAppName,
            opts =>
            {
                opts.Duration = TimeSpan.FromMinutes(2);
            });

        return services;
    }

    #region " Private Methods "
    static void AddAllDapperTypeHandlers()
    {
        var typeHandlers = InfrastructureAssemblyReference.Assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface &&
                            t.BaseType != null &&
                            t.BaseType.IsGenericType &&
                            t.BaseType.GetGenericTypeDefinition() == typeof(SqlMapper.TypeHandler<>))
                .ToList();

        foreach (var handlerType in typeHandlers)
        {
            var handledType = handlerType.BaseType.GetGenericArguments().First();
            var handlerInstance = Activator.CreateInstance(handlerType);

            SqlMapper.AddTypeHandler(handledType, (SqlMapper.ITypeHandler)handlerInstance);
        }
    }
    static SqlConnectionStringBuilder GetConnectionString(IConfiguration config, string configName, ApplicationIntent applicationIntent)
    {
        var result = config.GetSection($"Database:{configName}").Get<SqlConnectionStringBuilder>()!;

        if (result is null)
        {
            result = config.GetSection($"Database:{ServiceCollectionExtension.Default_Db_ConfigName}").Get<SqlConnectionStringBuilder>()!;
            if (result is null)
            {
                throw new InvalidOperationException($"can not find db config for '{configName}'");
            }
        }

        result.ApplicationIntent = applicationIntent;
        return result;
    }
    static string GetReadConnectionstring(IConfiguration config, string configName) => GetConnectionString(config, configName, ApplicationIntent.ReadOnly).ConnectionString;
    static string GetWriteConnectionstring(IConfiguration config, string configName) => GetConnectionString(config, configName, ApplicationIntent.ReadWrite).ConnectionString;
    static IServiceCollection AddReadWriteDbContext<TReadDbContext, TWriteDbContext>(
        this IServiceCollection services,
        IConfiguration config,
        string configName)
        where TReadDbContext : DbContext
        where TWriteDbContext : DbContext
    {
        services.AddDbContext<TReadDbContext>(o =>
        {
            o.UseSqlServer(GetReadConnectionstring(config, configName))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging(true)
                .EnableServiceProviderCaching(true);
        });

        services.AddDbContext<TWriteDbContext>(o =>
        {
            o.UseSqlServer(GetWriteConnectionstring(config, configName))
                .EnableSensitiveDataLogging(true)
                .EnableServiceProviderCaching(true);
        });

        return services;
    }
    #endregion

}