using ParsMedeq.Domain.Helpers;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using SRH.PrimitiveTypes.Optional;

namespace ParsMedeq.Infrastructure.Persistance.UnitOfWork;

public abstract class WriteUnitofWorkBase<TDbContext> : UnitofWorkBase<TDbContext>
    where TDbContext : DbContext
{
    private readonly static JsonSerializerSettings _defaultJsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };
    private IDbContextTransaction? _currentTransaction = null;

    #region " Constructors "
    public WriteUnitofWorkBase(
        TDbContext dbContext,
        IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
    {
        
    }
    #endregion

    #region " Methods "
    public async ValueTask<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (!CanStartTransaction() && this._currentTransaction is not null)
        {
            return this._currentTransaction;
        }
        this._currentTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        return this._currentTransaction;
    }
    public async ValueTask<PrimitiveResult<int>> SaveChangesAsync(bool autoCommit = true, CancellationToken cancellationToken = default)
    {
        var result = await this._dbContext.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);
        return PrimitiveResult.Success(result);
    }
    public ValueTask<PrimitiveResult<int>> SaveChangesAsync(CancellationToken cancellationToken = default) => this.SaveChangesAsync(true, cancellationToken);
    public IDbContextTransaction? GetCurrentTransaction() => this._currentTransaction;
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (this._currentTransaction is null) return;
        await this._currentTransaction!.CommitAsync(cancellationToken).ConfigureAwait(false);
        this._currentTransaction = null;
    }
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (this._currentTransaction is null) return;
        await this._currentTransaction!.RollbackAsync(cancellationToken).ConfigureAwait(false);
        this._currentTransaction = null;
    }
    protected bool CanStartTransaction() => this._currentTransaction is null && this._dbContext.Database.CurrentTransaction is null;
    #endregion
}
