using ParsMedeq.Domain;
using ParsMedeq.Domain.Types;
using System.Reflection;
using System.Text;

namespace ValueConvertersGenerator;
internal static class Generator
{
    static Dictionary<Type, string> _typeAliasDictionary = new Dictionary<Type, string>
        {
            { typeof(bool), "bool" },
            { typeof(byte), "byte" },
            { typeof(sbyte), "sbyte" },
            { typeof(char), "char" },
            { typeof(decimal), "decimal" },
            { typeof(double), "double" },
            { typeof(float), "float" },
            { typeof(int), "int" },
            { typeof(uint), "uint" },
            { typeof(long), "long" },
            { typeof(ulong), "ulong" },
            { typeof(object), "object" },
            { typeof(short), "short" },
            { typeof(ushort), "ushort" },
            { typeof(string), "string" },
            { typeof(DateTimeOffset), "DateTimeOffset" },
            { typeof(void), "void" }
        };
    static HashSet<string> TTTTT = new HashSet<string>();

    public static void Generate(string path)
    {
        var dbValueObjects = DomainAssemblyReference.Assembly.DefinedTypes
        .Where(t =>
            t is { IsAbstract: false, IsInterface: false }
            && t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition().Equals(typeof(IDbType<>))))
        .Select(t => new AA(t, t.Namespace, t.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition().Equals(typeof(IDbType<>)))
                .GetGenericArguments().First()
        ))
        .OrderBy(x => x.ValueObjectType.Name)
        .ToArray();

        var dbValueConvertersDirPath = Path.Combine(path, "EF");
        var dapperdbValueConvertersDirPath = Path.Combine(path, "dapper");

        System.IO.Directory.CreateDirectory(dbValueConvertersDirPath);
        System.IO.Directory.CreateDirectory(dapperdbValueConvertersDirPath);

        var i = 1;
        foreach (var valueType in dbValueObjects)
        {
            var fromIdMethod = valueType.ValueObjectType.GetMethod("FromId");
            var fromDbMethod = valueType.ValueObjectType.GetMethod("FromDb");

            var fromDbMethodName = fromDbMethod is not null
                ? fromDbMethod.Name
                : fromIdMethod is not null ? fromIdMethod.Name : string.Empty;

            if (string.IsNullOrWhiteSpace(fromDbMethodName))
            {
                Console.WriteLine($"can not find db method of '{valueType.ValueType.Name}'");
                continue;
            }
            var converterClassName = $"{valueType.ValueObjectType.Name}ValueConverter";
            var comparerClassName = $"{valueType.ValueObjectType.Name}ValueComparer";
            var dapperTypeHandlerClassName = $"{valueType.ValueObjectType.Name}DapperTypeMapper";

            var converterClass = GenerateValueConverter(valueType, fromDbMethodName, converterClassName);
            var comparerClass = GenerateValueComparer(valueType, comparerClassName);
            var dapperTypeMapperClass = GenerateDapperValueConverter(valueType, fromDbMethodName, dapperTypeHandlerClassName);

            using (System.IO.StreamWriter writer = new StreamWriter(@$"{dbValueConvertersDirPath}\{converterClassName}.cs", false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(converterClass);
                writer.WriteLine(string.Empty);
                writer.WriteLine(comparerClass);
                writer.Close();
            }

            using (System.IO.StreamWriter writer = new StreamWriter(@$"{dapperdbValueConvertersDirPath}\{dapperTypeHandlerClassName}.cs", false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(dapperTypeMapperClass);
                writer.WriteLine(string.Empty);
                writer.Close();
            }
            Console.WriteLine($"#{(i++)} {valueType.ValueObjectType.Name}");
        }
    }

    static string GenerateValueConverter(AA src, string fromDbMethodName, string className)
    {
        if (!_typeAliasDictionary.TryGetValue(src.ValueType, out var tName))
        {
            tName = src.ValueType.Name;
            Console.WriteLine($"can not find alias of '{tName}'");
        }
        TTTTT.Add(tName);

        var result = new StringBuilder();

        result.AppendLine($"global using {src.Namespace};");
        result.AppendLine();
        result.AppendLine("namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;");
        result.AppendLine($"sealed class {className} : ValueConverter<{src.ValueObjectType.Name}, {tName}>");
        result.AppendLine("{");
        result.AppendLine($"\tpublic {className}(): base(");
        result.AppendLine("\t\tsrc => src.GetDbValue(),");
        result.AppendLine($"\t\tvalue => {src.ValueObjectType.Name}.{fromDbMethodName}(value)");
        result.AppendLine("\t){}");
        result.AppendLine("}");

        return result.ToString();
    }
    static string GenerateValueComparer(AA src, string className)
    {
        var result = new StringBuilder();

        result.AppendLine($"sealed class {className} : ValueComparer<{src.ValueObjectType.Name}>");
        result.AppendLine("{");
        result.AppendLine($"\tpublic {className}(): base(");
        result.AppendLine("\t\t(a, b) => a.Equals(b),");
        result.AppendLine($"\t\ta => a.GetHashCode())");
        result.AppendLine("\t{}");
        result.AppendLine("}");

        return result.ToString();
    }
    static string GenerateDapperValueConverter(AA src, string fromDbMethodName, string className)
    {
        static string GenerateParser(string clrType)
        {
            return clrType.ToLower().Trim() switch
            {
                "bool" => "Convert.ToBoolean(value)",
                "byte" => "Convert.ToByte(value)",
                "sbyte" => "Convert.ToSByte(value)",
                "char" => "Convert.ToChar(value)",
                "decimal" => "Convert.ToDecimal(value)",
                "double" => "Convert.ToDouble(value)",
                "float" => "Convert.ToDouble(value)",
                "int" => "Convert.ToInt32(value)",
                "uint" => "Convert.ToUInt32(value)",
                "long" => "Convert.ToInt64(value)",
                "ulong" => "Convert.ToUInt64(value)",
                "object" => "value",
                "short" => "Convert.ToInt16(value)",
                "ushort" => "Convert.ToUInt16(value)",
                "string" => "Convert.ToString(value)",
                "datetimeoffset" => "DateTimeOffset.Parse(value is null ? DateTimeOffset.MinValue.ToString() : value!.ToString())",
                _ => ""
            };
        }
        if (!_typeAliasDictionary.TryGetValue(src.ValueType, out var tName))
        {
            tName = src.ValueType.Name;
            Console.WriteLine($"can not find alias of '{tName}'");
        }

        var result = new StringBuilder();

        result.AppendLine($"global using {src.Namespace};");
        result.AppendLine();
        result.AppendLine("namespace ParsMedeq.Infrastructure.Persistance.DapperValueConverters;");
        result.AppendLine($"sealed class {className} : SqlMapper.TypeHandler<{src.ValueObjectType.Name}>");
        result.AppendLine("{");
        result.AppendLine($"\tpublic override {src.ValueObjectType.Name} Parse(object value) => {src.ValueObjectType.Name}.{fromDbMethodName}({GenerateParser(tName)});");
        result.AppendLine($"\tpublic override void SetValue(IDbDataParameter parameter, {src.ValueObjectType.Name} value) => parameter.Value = value.GetDbValue();");
        result.AppendLine("}");

        return result.ToString();
    }
    record AA(TypeInfo ValueObjectType, string? Namespace, Type ValueType);
}
