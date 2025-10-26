using System.Collections.Concurrent;

namespace ParsMedeQ.Infrastructure.Persistance.DbContexts.Extensions;
public static class DbContextExtension
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
    public static async ValueTask<PrimitiveResult<PaginatedResult<TResult, TKey>>> PaginateByPrimaryKey<TEntity, TKey, TResult>(
        this DbContext src,
        IQueryable<TEntity> query,
        TKey? lastSeenKey,
        Expression<Func<TEntity, bool>>? predicate,
        int pageSize,
        Expression<Func<TEntity, TResult>>? projection,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        var primaryKeysResult = src.GetPrimaryKeys<TEntity>();

        if ((primaryKeysResult?.Length ?? 0) == 0)
            return PrimitiveResult.Failure<PaginatedResult<TResult, TKey>>(GenericPrimitiveReadRepositoryErrors.Generate_Entity_PrimaryKey_Not_Found_Error<TEntity>());

        var primaryKey = primaryKeysResult!.First();

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        //var queryString = query.ToQueryString();

        var totalCount = await query.LongCountAsync(cancellationToken).ConfigureAwait(false);

        // Expression: x => x.PK > lastSeenKey (if lastSeenKey != null)
        if (lastSeenKey is not null)
        {
            var param = Expression.Parameter(typeof(TEntity), "x");
            var pkAccess = Expression.Property(param, primaryKey);
            Expression comparison;

            if (pkAccess.Type == typeof(string))
            {
                var lastSeenConst = Expression.Constant(lastSeenKey, typeof(string));
                var compareToCall = Expression.Call(pkAccess, typeof(string).GetMethod(nameof(string.CompareTo), new[] { typeof(string) })!, lastSeenConst);
                comparison = Expression.GreaterThan(compareToCall, Expression.Constant(0));
            }
            else
            {
                // Numeric, DateTime, Guid, etc.
                var lastSeenConst = Expression.Constant(lastSeenKey, pkAccess.Type);
                comparison = Expression.GreaterThan(pkAccess, lastSeenConst);
            }

            var lambda = Expression.Lambda<Func<TEntity, bool>>(comparison, param);
            query = query.Where(lambda);
        }


        query = query.OrderByPropertyName(primaryKey, ascending: true).Take(pageSize);
        var projectedResult = projection is not null
            ? query.Select(projection!)
            : query.Cast<TResult>();
        //queryString = query.ToQueryString();
        var resultArray = await projectedResult.ToArrayAsync(cancellationToken).ConfigureAwait(false) ?? [];

        TKey? lastKey = default;

        if (resultArray.Length > 0)
        {
            var lastEntity = resultArray.Last()!;
            var keyProp = typeof(TResult).GetProperty(primaryKey)!;
            lastKey = (TKey)keyProp.GetValue(lastEntity)!;
        }

        return new PaginatedResult<TResult, TKey>(totalCount, lastKey!, resultArray);
    }
}
public sealed record PaginatedResult<TResult, TKey>(long TotalCount, TKey LastSeen, TResult[] Data);