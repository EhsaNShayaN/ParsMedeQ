using System.Reflection;

namespace ParsMedeq.Infrastructure;

public static class InfrastructureAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}