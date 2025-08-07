using Microsoft.EntityFrameworkCore.Storage;

namespace EShop.Application.Persistance;

public interface IWriteUnitOfWork : IUnitOfWork
{
    ValueTask<PrimitiveResult<int>> SaveChangesAsync(bool autoCommit, CancellationToken cancellationToken = default);
    ValueTask<PrimitiveResult<int>> SaveChangesAsync(CancellationToken cancellationToken = default);
    ValueTask<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

    IDbContextTransaction? GetCurrentTransaction();

}
