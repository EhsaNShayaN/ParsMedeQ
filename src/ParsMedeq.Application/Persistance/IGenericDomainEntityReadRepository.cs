namespace ParsMedeQ.Application.Persistance;

public interface IGenericDomainEntityReadRepository<TEntity>
{
    ValueTask<PrimitiveResult<TResult[]>> Filter<TResult>(
       Expression<Func<TEntity, bool>> predicate,
       Expression<Func<TEntity, object>>? sort,
       Expression<Func<TEntity, TResult>>? projection,
       CancellationToken cancellationToken);

    ValueTask<PrimitiveResult<TResult>> GetOne<TResult>(
       Expression<Func<TEntity, bool>> predicate,
       Expression<Func<TEntity, TResult>>? projection,
       PrimitiveError primitiveError,
       CancellationToken cancellationToken);

    ValueTask<PrimitiveResult<TResult>> GetOneOrDefault<TResult>(
       Expression<Func<TEntity, bool>> predicate,
       Expression<Func<TEntity, TResult>>? projection,
       TResult defaultValue,
       CancellationToken cancellationToken);
}

