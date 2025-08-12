using System.Reflection;

namespace ParsMedeq.Domain;

public static class DomainAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}