using System.Reflection;

namespace EShop.Infrastructure;

public static class InfrastructureAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}