using System.Reflection;

namespace ParsMedeQ.Application;

public static class ApplicationAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}