namespace SRH.Utilities.Helpers;

public static class TypeHelpers
{
    public static IEnumerable<T> GetAllStaticFieldsOfType<T>()
    {
        var enumerationType = typeof(T);

        return enumerationType
            .GetFields(
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.FlattenHierarchy
            ).Where(fieldInfo =>
                enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fielInfo => (T)fielInfo.GetValue(default)!);
    }
}
