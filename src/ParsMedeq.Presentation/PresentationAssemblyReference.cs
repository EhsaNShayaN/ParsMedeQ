using System.Reflection;

namespace EShop.Presentation;

public static class PresentationAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}