using System.Collections.Concurrent;

namespace SRH.Persistance.Extensions;

public static class DbContextExtensions
{
    private static ConcurrentDictionary<string, string[]> _cachedPrimaryKeys = new ConcurrentDictionary<string, string[]>(StringComparer.InvariantCulture);

    public readonly static string[] EmptyPrimaryKey = Array.Empty<string>();

    public static IDictionary<string, object> GetPrimaryKeysAndValues<TEntity>(this DbContext ctx, TEntity entity) where TEntity : class
    {
        if (ctx is null)
        {
            throw new ArgumentNullException(nameof(ctx));
        }

        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var primaryKeys = ctx.GetPrimaryKeys<TEntity>();

        if (!primaryKeys.Any()) return new Dictionary<string, object>();

        var entityProperties = entity.GetType().GetProperties().ToArray();
        var result = primaryKeys.ToDictionary(x => x, x => entityProperties.First(a => a.Name.Equals(x)).GetValue(entity)!)!;
        return result!;
    }
    public static string[] GetPrimaryKeys(this DbContext ctx, Type entityType)
    {
        if (ctx is null)
        {
            throw new ArgumentNullException(nameof(ctx));
        }

        if (_cachedPrimaryKeys.TryGetValue(entityType.ToString(), out var r)) return r;

        var pks = ctx.Model.FindEntityType(entityType)?.FindPrimaryKey();

        if (pks is null)
        {
            return EmptyPrimaryKey;
        }

        var result = pks.Properties.Select(p => p.Name).ToArray();

        _cachedPrimaryKeys.TryAdd(entityType.ToString(), result!);

        return result!;
    }
    public static string[] GetPrimaryKeys<TEntity>(this DbContext ctx) where TEntity : class => ctx.GetPrimaryKeys(typeof(TEntity));

    public static IEnumerable<string> GetDirtyProperties(this DbContext ctx, object entity)
    {
        if (ctx == null)
        {
            throw new ArgumentNullException(nameof(ctx));
        }

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var entry = ctx.Entry(entity);
        var originalValues = entry.OriginalValues;
        var currentValues = entry.CurrentValues;

        foreach (var prop in originalValues.Properties)
        {
            if (Equals(originalValues[prop.Name], currentValues[prop.Name]) == false)
            {
                yield return prop.Name;
            }
        }
    }

    public static void Reset(this DbContext ctx, object entity)
    {
        if (ctx == null)
        {
            throw new ArgumentNullException(nameof(ctx));
        }

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var entry = ctx.Entry(entity);
        var originalValues = entry.OriginalValues;
        var currentValues = entry.CurrentValues;

        foreach (var prop in originalValues.Properties)
        {
            currentValues[prop.Name] = originalValues[prop.Name];
        }

        entry.State = EntityState.Unchanged;
    }

    public static string GetSchemaAndTableName<T>(this DbContext src)
    {
        var entityType = src.Model.FindEntityType(typeof(T));
        if (entityType is null) throw new Exception($"Can not find DbSet of '{typeof(T)}'");
        var schema = entityType.GetSchema();
        var tableName = entityType.GetTableName();

        if (tableName is null) throw new Exception($"Can not find TableName of '{typeof(T)}'");
        return $"[{schema ?? "dbo"}].[{tableName}]";
    }
}