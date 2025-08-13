using SRH.ServiceInstaller;
using System.Reflection;

namespace ParsMedeQ.Server.DI;

public sealed class ProjectServiceInstaller : IServiceInstaller
{
    public Assembly[]? DependantAssemblies => [
        Application.ApplicationAssemblyReference.Assembly,
        Infrastructure.InfrastructureAssemblyReference.Assembly,
        Presentation.PresentationAssemblyReference.Assembly,
    ];

    public IServiceCollection InstallService(IServiceCollection services, IConfiguration config) =>
        services.AddMinimalEndpoints(this.DependantAssemblies!);
}
