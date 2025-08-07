namespace SRH.Persistance.Repositories.Write;
public abstract partial class GenericPrimitiveWriteRepositoryBase<TDbContext>
{
    public virtual async ValueTask<PrimitiveResult<int>> ExecuteAsync(CommandDefinition command) =>
       PrimitiveMaybe.From(await this.GetDbConnection().ExecuteAsync(command).ConfigureAwait(false))
           .Map(data => PrimitiveResult.Success(data))
           .GetOr(PrimitiveResult.Success(0));

    public virtual async ValueTask<PrimitiveResult<T>> ExecuteAsync<T>(
        CommandDefinition command,
        Func<int, ValueTask<PrimitiveResult<T>>> onSucces,
        Func<ValueTask<PrimitiveResult<T>>> onZeroAffected) where T : class
    {
        var rowsAffected = await this.GetDbConnection().ExecuteAsync(command).ConfigureAwait(false);

        return rowsAffected switch
        {
            (> 0) => await onSucces.Invoke(rowsAffected).ConfigureAwait(false),
            _ => await onZeroAffected.Invoke().ConfigureAwait(false)
        };
    }
}

