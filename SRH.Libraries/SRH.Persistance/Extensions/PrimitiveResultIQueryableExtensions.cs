namespace SRH.Persistance.Extensions;

public static class PrimitiveResultIQueryableExtensions
{
    public static async ValueTask<PrimitiveResult<TResult>> Run<TEntity, TResult>(
        this IQueryable<TEntity> queryable,
        Func<IQueryable<TEntity>, Task<TResult>> func,
        PrimitiveError nullError) where TResult : notnull
    {
        var dbResult = await func.Invoke(queryable).ConfigureAwait(false);

        return PrimitiveMaybe.From(dbResult)
            .Map(x => PrimitiveResult.Success(x))
            .GetOr(PrimitiveResult.Failure<TResult>(nullError));
    }

    public static async ValueTask<PrimitiveResult<TResult>> Run<TEntity, TResult>(
       this IQueryable<TEntity> queryable,
       Func<IQueryable<TEntity>, Task<TResult>> func,
       PrimitiveResult<TResult> defaultValue) where TResult : notnull
    {
        var dbResult = await func.Invoke(queryable).ConfigureAwait(false);

        return PrimitiveMaybe.From(dbResult)
            .Map(x => PrimitiveResult.Success(x))
            .GetOr(defaultValue);
    }

    public static async ValueTask<PrimitiveResult<PaginateListResult<TEntity>>> Paginate<TEntity, TKey>(
        this IQueryable<TEntity> query,
        PaginateQuery paginateQuery,
        Expression<Func<TEntity, TKey>> keySelector,
        PaginateOrder order,
        CancellationToken cancellationToken)
    {
        query = order.Equals(PaginateOrder.ASC) ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);

        var totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

        var data = await query
           .Skip(paginateQuery.PageIndex * paginateQuery.PageSize)
           .Take(paginateQuery.PageSize)
           .ToListAsync(cancellationToken)
           .ConfigureAwait(false);

        return PrimitiveResult.Success(
            PaginateListResult<TEntity>.Create(
                data,
                totalCount));
    }

    public static async ValueTask<PrimitiveResult<PaginateListResult<TEntity>>> PaginateOverPK<TEntity, TKey>(
        this IQueryable<TEntity> query,
        int rowsCount,
        string pkName,
        TKey lastId,
        PaginateOrder order,
        CancellationToken cancellationToken) where TKey : IComparable<TKey>
    {
        query = order.Equals(PaginateOrder.ASC)
            ? query.OrderBy(e => EF.Property<TKey>(e, pkName))
            : query.OrderByDescending(e => EF.Property<TKey>(e, pkName));

        var whereClause = CreateIdComparisonExpression<TEntity, TKey>(lastId, order == PaginateOrder.ASC, pkName);

        var mainQuery = query.Where(whereClause);

        var totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

        var data = await mainQuery
           .Take(rowsCount)
           .ToListAsync(cancellationToken)
           .ConfigureAwait(false);

        return PrimitiveResult.Success(
            PaginateListResult<TEntity>.Create(
                data,
                totalCount));
    }

    public static ValueTask<PrimitiveResult<PaginateListResult<TEntity>>> PaginateOverPK<TEntity, TKey>(
       this IQueryable<TEntity> query,
       int rowsCount,
       TKey lastId,
       PaginateOrder order,
       CancellationToken cancellationToken) where TKey : IComparable<TKey> =>
        query.PaginateOverPK(rowsCount, "Id", lastId, order, cancellationToken);

    static Expression<Func<TEntity, bool>> CreateIdComparisonExpression<TEntity, TKey>(
        TKey lastId,
        bool isGreaterThan,
        string pkName = "Id")
        where TKey : IComparable<TKey>
    {
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var idProperty = Expression.Property(parameter, pkName);

        var lastIdConstant = Expression.Constant(lastId, typeof(TKey));

        var comparison = isGreaterThan
            ? Expression.GreaterThan(idProperty, lastIdConstant) // x.Id > lastId
            : Expression.LessThan(idProperty, lastIdConstant);   // x.Id < lastId

        var lambda = Expression.Lambda<Func<TEntity, bool>>(comparison, parameter);

        return lambda;
    }

}
