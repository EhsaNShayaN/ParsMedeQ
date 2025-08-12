using System.Reflection;

namespace ParsMedeq.Application;

public static class ApplicationAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}