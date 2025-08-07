using EShop.Application.Persistance;
using EShop.Infrastructure.Persistance.DbContexts;

namespace EShop.Infrastructure.Schemas;

internal abstract class GenericDomainEntityReadRepositoryBase<TEntity> :
    GenericPrimitiveReadRepositoryBase<EshopReadDbContext>,
    IGenericDomainEntityReadRepository<TEntity>
    where TEntity : class
{
    protected GenericDomainEntityReadRepositoryBase(EshopReadDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<TResult[]>> Filter<TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? sort,
        Expression<Func<TEntity, TResult>>? projection,
        CancellationToken cancellationToken)
    {
        return this.ToArrayAsync(
            predicate,
            sort,
            projection,
            cancellationToken);
    }

    public ValueTask<PrimitiveResult<TResult>> GetOne<TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>>? projection,
        PrimitiveError primitiveError,
        CancellationToken cancellationToken)
    {
        return this.FirstOrDefaultAsync(
             predicate,
             null,
             projection,
             primitiveError,
             cancellationToken);
    }

    public ValueTask<PrimitiveResult<TResult>> GetOneOrDefault<TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>>? projection,
        TResult defaultValue,
        CancellationToken cancellationToken)
    {
        return this.FirstOrDefaultAsync(
            predicate,
            null,
            projection,
            defaultValue,
            cancellationToken);
    }
}
