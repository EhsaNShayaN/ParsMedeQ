using ParsMedeQ.Domain;
using System.Reflection;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts;
public abstract class DbContextBase<T> : DbContext
    where T : DbContext
{
    readonly static Type _converterType = typeof(ValueConverter);
    readonly static Type _comparerType = typeof(ValueComparer);
    readonly static Assembly[] _assemblies = [InfrastructureAssemblyReference.Assembly, DomainAssemblyReference.Assembly];
    readonly static TypeInfo[] _allAssemblyConverterAndComparerTypes = _assemblies.SelectMany(assembly =>
            assembly.DefinedTypes.Where(t =>
                t is { IsAbstract: false, IsInterface: false }
                && (t.IsSubclassOf(_converterType) || t.IsSubclassOf(_comparerType)))).ToArray();

    readonly static TypeInfo[] _allValueConverters = _allAssemblyConverterAndComparerTypes.Where(t => t.IsSubclassOf(_converterType)).ToArray();
    readonly static TypeInfo[] _allvalueComparers = _allAssemblyConverterAndComparerTypes.Where(t => t.IsSubclassOf(_comparerType)).ToArray();
    readonly static Dictionary<Type, string> _allDbTypeAliases = new()
    {
        { typeof(EmailType), DbValueTypeCreator.Varchar(255) },
        { typeof(MobileType), DbValueTypeCreator.Varchar(11) },
        { typeof(FirstNameType), DbValueTypeCreator.NVarchar(150) },
        { typeof(LastNameType), DbValueTypeCreator.Varchar(150) },
    };

    public DbContextBase(DbContextOptions<T> opts) : base(opts) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var dbContextAssembly = typeof(T).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(dbContextAssembly);

    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        ApplyAllConverters(configurationBuilder);
    }

    internal static void ApplyAllConverters(ModelConfigurationBuilder configurationBuilder)
    {
        foreach (var converter in _allValueConverters)
        {
            var genericArgs = converter.BaseType!.GetGenericArguments();
            if (genericArgs.Length != 2) continue;
            var comparer = _allvalueComparers
                .FirstOrDefault(x => x.BaseType!.GetGenericArguments().First().Equals(genericArgs.First()));

            if (_allDbTypeAliases.TryGetValue(
                IntrospectionExtensions.GetTypeInfo(genericArgs.First()),
                out var dbAliasType) && !string.IsNullOrWhiteSpace(dbAliasType))
            {
                configurationBuilder.Properties(
                    genericArgs.First(),
                    builder => builder.HaveColumnType(dbAliasType)
                        .HaveConversion(converter, comparer));
            }
            else
            {
                configurationBuilder.Properties(
                        genericArgs.First(),
                        builder => builder.HaveConversion(converter, comparer));
            }
        }
    }
}
internal abstract class DbValueTypeCreator
{
    private readonly static DbValueTypeCreator _varchar = new VarcharDbValueTypeCreator();
    private readonly static DbValueTypeCreator _nVarchar = new NVarcharDbValueTypeCreator();

    public static string Varchar(int size) => $"{_varchar.ValueType}({size})";
    public static string NVarchar(int size) => $"{_nVarchar.ValueType}({size})";

    protected abstract string ValueType { get; }

    class VarcharDbValueTypeCreator : DbValueTypeCreator
    {
        protected override string ValueType => "varchar";
    }
    class NVarcharDbValueTypeCreator : DbValueTypeCreator
    {
        protected override string ValueType => "nvarchar";
    }
}