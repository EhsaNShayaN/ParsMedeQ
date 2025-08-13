using SRH.ServiceInstaller;
using System.Reflection;

namespace ParsMedeQ.Server.DI;

public sealed class ProjectServiceInstaller : IServiceInstaller
{
    public Assembly[]? DependantAssemblies => [
        ParsMedeQ.Application.ApplicationAssemblyReference.Assembly,
        ParsMedeQ.Infrastructure.InfrastructureAssemblyReference.Assembly];

    public IServiceCollection InstallService(IServiceCollection services, IConfiguration config) => services;
}
