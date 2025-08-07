using System.Reflection;

namespace EShop.Domain;

public static class DomainAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}