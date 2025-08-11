using SRH.ServiceInstaller;
using System.Reflection;

namespace ParsMedeq.Server.DI;

public sealed class ProjectServiceInstaller : IServiceInstaller
{
    public Assembly[]? DependantAssemblies => [
        EShop.Application.ApplicationAssemblyReference.Assembly,
        EShop.Infrastructure.InfrastructureAssemblyReference.Assembly];

    public IServiceCollection InstallService(IServiceCollection services, IConfiguration config) => services;
}
