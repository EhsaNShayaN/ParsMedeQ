using System.Reflection;

namespace ParsMedeq.Server;

public static class ApiAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}