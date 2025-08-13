using System.Reflection;

namespace ParsMedeQ.Server;

public static class ApiAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}