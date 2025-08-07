namespace SRH.Sql.ConfigProvider;
public sealed class DbConfigSourceContext : DbContext
{
    static readonly Dictionary<Type, Func<PropertiesConfigurationBuilder, PropertiesConfigurationBuilder>> _ValueObjectDataTypes = new()
    {
        { typeof(SettingVersion), a => a.HaveColumnType("varchar(100)") },
        { typeof(SettingApplicationName), a => a.HaveColumnType("varchar(100)").AreUnicode(false) },
        { typeof(SettingKey), a => a.HaveColumnType("varchar(255)").AreUnicode(false) }
    };
    private readonly string _connectionstring;

    public DbSet<Settings> Settings { get; set; }

    public DbConfigSourceContext(string connectionstring)
    {
        _connectionstring = connectionstring;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionstring);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        ApplyAllConverters(configurationBuilder, Assembly.GetAssembly(typeof(DbConfigSourceContext))!);
        base.ConfigureConventions(configurationBuilder);
    }

    private void ApplyAllConverters(ModelConfigurationBuilder configurationBuilder, params Assembly[] assemblies)
    {
        var converterType = typeof(ValueConverter);
        var comprareType = typeof(ValueComparer);
        var allAssemblyTypes = assemblies.SelectMany(assembly =>
            assembly.DefinedTypes.Where(t =>
                t is { IsAbstract: false, IsInterface: false }
                && (t.IsSubclassOf(converterType) || t.IsSubclassOf(comprareType)))).ToArray();

        var allValueConverters = allAssemblyTypes.Where(t => t.IsSubclassOf(converterType)).ToList();
        var allvalueComparers = allAssemblyTypes.Where(t => t.IsSubclassOf(comprareType)).ToList();
        foreach (var converter in allValueConverters)
        {
            var genericArgs = converter.BaseType!.GetGenericArguments();
            if (genericArgs.Length != 2) continue;
            var comparer = allvalueComparers.FirstOrDefault(x => x.BaseType!.GetGenericArguments().First().Equals(genericArgs.First()));
            if (_ValueObjectDataTypes.TryGetValue(genericArgs.First(), out var customBuilder) && customBuilder is not null)
            {
                configurationBuilder.Properties(
                    genericArgs.First(),
                    builder => customBuilder(builder.HaveConversion(converter, comparer)));
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
