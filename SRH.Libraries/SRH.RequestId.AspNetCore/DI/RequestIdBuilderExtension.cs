using SRH.RequestId.Providers;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SRH.RequestId.AspNetCore.DI;

public static class RequestIdBuilderExtension
{
    public static IRequestIdBuilder WithGuidProvider(this IRequestIdBuilder src)
    {
        src.Services.TryAddSingleton<IRequestIdProvider, GuidRequestIdProvider>();
        return src;
    }

    public static IRequestIdBuilder WithTraceIdProvider(this IRequestIdBuilder src)
    {
        src.Services.TryAddSingleton<IRequestIdProvider, TraceIdRequestIdProvider>();
        return src;
    }

    public static IRequestIdBuilder WithCustomRequestIdProvider(this IRequestIdBuilder src, IRequestIdProvider provider)
    {
        src.Services.TryAddSingleton(provider);
        return src;
    }
    
    public static IRequestIdBuilder WithCustomRequestIdProvider<T>(this IRequestIdBuilder src) where T : class,IRequestIdProvider
    {
        src.Services.TryAddSingleton<IRequestIdProvider, T>();
        return src;
    }
}