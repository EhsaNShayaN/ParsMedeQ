using System.Reflection;

namespace ParsMedeQ.Domain;

public static class DomainAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}