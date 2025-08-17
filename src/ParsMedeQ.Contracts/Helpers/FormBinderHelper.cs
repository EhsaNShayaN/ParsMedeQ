using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using System.Reflection;

namespace ParsMedeQ.Contracts.Helpers;
internal static class FormBinderHelper
{
    static ConcurrentDictionary<string, PropertyInfo[]> _reflectedTypeProperties = new(StringComparer.OrdinalIgnoreCase);

    public static T Bind<T>(IFormCollection form) where T : new()
    {
        var obj = new T();
        var props = GetOrSetTypeProperties(typeof(T));

        foreach (var prop in props)
        {
            if (!form.TryGetValue(prop.Name, out var value))
                continue;

            if (string.IsNullOrWhiteSpace(value))
                continue;

            try
            {
                object? converted = Convert.ChangeType(value.ToString(), prop.PropertyType);
                prop.SetValue(obj, converted);
            }
            catch
            {
                // ignore if conversion fails
            }
        }

        return obj;
    }

    static PropertyInfo[] GetOrSetTypeProperties(Type type)
    {
        var key = type.FullName!;

        if (_reflectedTypeProperties.TryGetValue(key, out var r) && r is not null)
            return r;

        var result = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        _reflectedTypeProperties.TryAdd(key, result ?? []);

        return result ?? [];

    }
}
