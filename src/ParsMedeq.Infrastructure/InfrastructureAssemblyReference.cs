using System.Reflection;

namespace ParsMedeQ.Infrastructure;

public static class InfrastructureAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}