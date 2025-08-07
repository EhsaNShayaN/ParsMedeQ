using Microsoft.Extensions.DependencyInjection;

namespace SRH.RequestId.AspNetCore.DI;

public interface IRequestIdBuilder 
{
    IServiceCollection Services { get; }
}

internal sealed class RequestIdServiceBuilder : IRequestIdBuilder
{
    public IServiceCollection Services { get; } = null!;

    public RequestIdServiceBuilder(IServiceCollection services) => this.Services = services;
}
