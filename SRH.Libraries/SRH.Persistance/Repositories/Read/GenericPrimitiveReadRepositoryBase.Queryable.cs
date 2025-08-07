using SRH.Persistance.Extensions;

namespace SRH.Persistance.Repositories.Read;
public abstract partial class GenericPrimitiveReadRepositoryBase<TDbContext>
{
    public virtual ValueTask<PrimitiveResult<PaginateListResult<TEntity>>> PaginateAsync<TEntity, TKey>(
       IQueryable<TEntity> query,
       PaginateQuery paginateQuery,
       Expression<Func<TEntity, TKey>> keySelector,
       PaginateOrder order,
       CancellationToken cancellationToken) =>
       query.Paginate(paginateQuery, keySelector, order, cancellationToken);
}
