using System.Reflection;

namespace EShop.Application;

public static class ApplicationAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}