using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SRH.Persistance.Repositories.Write;
public abstract partial class GenericPrimitiveWriteRepositoryBase<TDbContext> : GenericPrimitiveReadRepositoryBase<TDbContext>
    where TDbContext : DbContext
{
    protected GenericPrimitiveWriteRepositoryBase(TDbContext dbContext) : base(dbContext)
    {
    }

    #region " Add "
    public virtual ValueTask<PrimitiveResult<T>> Add<T>(T entity) where T : class =>
        BeginTrackEntity(entity, (e, _) => ValueTask.FromResult(this.DbContext.Add(e)));
    public virtual ValueTask<PrimitiveResult<T>> AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class =>
        BeginTrackEntity(entity, (e, _) => this.DbContext.AddAsync(entity, cancellationToken), cancellationToken);
    #endregion

    #region " AddRange "
    public virtual ValueTask<PrimitiveResult> AddRange<T>(params T[] entities) where T : class =>
        BeginTrackEntities(entities, (e, _) =>
        {
            this.DbContext.AddRange(e);
            return Task.CompletedTask;
        });
    public virtual ValueTask<PrimitiveResult> AddRange<T>(IEnumerable<T> entities) where T : class =>
        BeginTrackEntities(entities, (e, _) =>
        {
            this.DbContext.AddRange(e);
            return Task.CompletedTask;
        });
    public virtual ValueTask<PrimitiveResult> AddRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken) where T : class =>
        BeginTrackEntities(entities, async (e, _) => await this.DbContext.AddRangeAsync(e, cancellationToken));
    #endregion

    #region " Update "
    public virtual ValueTask<PrimitiveResult<T>> Update<T>(T entity) where T : class =>
        BeginTrackEntity(entity, (e, _) => ValueTask.FromResult(this.DbContext.Update(e)));
    #endregion

    #region " UpdateRange "
    public virtual ValueTask<PrimitiveResult> UpdateRange<T>(params T[] entities) where T : class =>
        BeginTrackEntities(entities, (e, _) =>
        {
            this.DbContext.UpdateRange(e);
            return Task.CompletedTask;
        });
    public virtual ValueTask<PrimitiveResult> UpdateRange<T>(IEnumerable<T> entities) where T : class =>
        BeginTrackEntities(entities, (e, _) =>
        {
            this.DbContext.UpdateRange(e);
            return Task.CompletedTask;
        });
    #endregion

    #region " Remove "
    public virtual ValueTask<PrimitiveResult<T>> Remove<T>(T entity) where T : class =>
        BeginTrackEntity(entity, (e, _) => ValueTask.FromResult(this.DbContext.Remove(e)));
    #endregion

    #region " RemoveRange "
    public virtual ValueTask<PrimitiveResult> RemoveRange<T>(params T[] entities) where T : class =>
        BeginTrackEntities(entities, (e, _) =>
        {
            this.DbContext.RemoveRange(e);
            return Task.CompletedTask;
        });
    public virtual ValueTask<PrimitiveResult> RemoveRange<T>(IEnumerable<T> entities) where T : class =>
        BeginTrackEntities(entities, (e, _) =>
        {
            this.DbContext.RemoveRange(e);
            return Task.CompletedTask;
        });
    #endregion

    #region " Private Methods "
    protected static ValueTask<PrimitiveResult<T>> BeginTrackEntity<T>(T entity,
    Func<T, CancellationToken, ValueTask<EntityEntry<T>>> func,
    CancellationToken cancellationToken = default) where T : class =>
        PrimitiveMaybe.From(entity)
        .Map(
            async model =>
            {
                var dbResult = await func.Invoke(entity, cancellationToken).ConfigureAwait(false);
                return PrimitiveMaybe.From(dbResult)
                    .MapValue(e => PrimitiveResult.Success(e.Entity))
                    .GetOr(GenericPrimitiveWriteRepositoryErrors.Generate_Null_Entity_Entry_Error<T>());
            }
        )
        .GetOr(GenericPrimitiveWriteRepositoryErrors.Generate_InvalidOperation_On_Null_Entity_Error<T>());

    protected static ValueTask<PrimitiveResult> BeginTrackEntities<T>(IEnumerable<T> entities,
        Func<IEnumerable<T>, CancellationToken, Task> act,
        CancellationToken cancellationToken = default) where T : class =>
        PrimitiveMaybe.From(entities)
        .Map(
            async model =>
            {
                await act.Invoke(entities, cancellationToken).ConfigureAwait(false);
                return PrimitiveResult.Success();
            })
        .GetOr(GenericPrimitiveWriteRepositoryErrors.Generate_InvalidOperation_On_Null_Entity_Error());

    #endregion
}
