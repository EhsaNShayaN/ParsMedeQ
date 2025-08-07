namespace SRH.Persistance.Repositories.Read;
public abstract partial class GenericPrimitiveReadRepositoryBase<TDbContext>
    where TDbContext : DbContext
{
    public async ValueTask<PrimitiveResult<TResult>> RunAsyncCompiledQuery<TResult>(Func<TDbContext, CancellationToken, Task<TResult>> compiledQuery, CancellationToken cancellationToken)
    {
        var compiledQueryResult = await compiledQuery.Invoke(this.DbContext, cancellationToken).ConfigureAwait(false);

        if (compiledQueryResult is null) return PrimitiveResult.Failure<TResult>(GenericPrimitiveReadRepositoryErrors.Null_DbResult_Error);

        return PrimitiveResult.Success(compiledQueryResult);
    }

    public async ValueTask<PrimitiveResult<TResult>> RunAsyncCompiledQuery<TParam1, TResult>(Func<TDbContext, TParam1, CancellationToken, Task<TResult>> compiledQuery,
        TParam1 param1,
        CancellationToken cancellationToken)
    {
        var compiledQueryResult = await compiledQuery.Invoke(this.DbContext, param1, cancellationToken).ConfigureAwait(false);

        if (compiledQueryResult is null) return PrimitiveResult.Failure<TResult>(GenericPrimitiveReadRepositoryErrors.Null_DbResult_Error);

        return PrimitiveResult.Success(compiledQueryResult);
    }

    public async ValueTask<PrimitiveResult<TResult>> RunAsyncCompiledQuery<TParam1, TParam2, TResult>(Func<TDbContext, TParam1, TParam2, CancellationToken, Task<TResult>> compiledQuery,
       TParam1 param1,
       TParam2 param2,
       CancellationToken cancellationToken)
    {
        var compiledQueryResult = await compiledQuery.Invoke(this.DbContext, param1, param2, cancellationToken).ConfigureAwait(false);

        if (compiledQueryResult is null) return PrimitiveResult.Failure<TResult>(GenericPrimitiveReadRepositoryErrors.Null_DbResult_Error);

        return PrimitiveResult.Success(compiledQueryResult);
    }

    public async ValueTask<PrimitiveResult<TResult>> RunAsyncCompiledQuery<TParam1, TParam2, TParam3, TResult>(Func<TDbContext, TParam1, TParam2, TParam3, CancellationToken, Task<TResult>> compiledQuery,
      TParam1 param1,
      TParam2 param2,
      TParam3 param3,
      CancellationToken cancellationToken)
    {
        var compiledQueryResult = await compiledQuery.Invoke(this.DbContext, param1, param2, param3, cancellationToken).ConfigureAwait(false);

        if (compiledQueryResult is null) return PrimitiveResult.Failure<TResult>(GenericPrimitiveReadRepositoryErrors.Null_DbResult_Error);

        return PrimitiveResult.Success(compiledQueryResult);
    }

    public async ValueTask<PrimitiveResult<TResult>> RunAsyncCompiledQuery<TParam1, TParam2, TParam3, TParam4, TResult>(Func<TDbContext, TParam1, TParam2, TParam3, TParam4, CancellationToken, Task<TResult>> compiledQuery,
      TParam1 param1,
      TParam2 param2,
      TParam3 param3,
      TParam4 param4,
      CancellationToken cancellationToken)
    {
        var compiledQueryResult = await compiledQuery.Invoke(this.DbContext, param1, param2, param3, param4, cancellationToken).ConfigureAwait(false);

        if (compiledQueryResult is null) return PrimitiveResult.Failure<TResult>(GenericPrimitiveReadRepositoryErrors.Null_DbResult_Error);

        return PrimitiveResult.Success(compiledQueryResult);
    }

    public async ValueTask<PrimitiveResult<TResult>> RunAsyncCompiledQuery<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>(Func<TDbContext, TParam1, TParam2, TParam3, TParam4, TParam5, CancellationToken, Task<TResult>> compiledQuery,
      TParam1 param1,
      TParam2 param2,
      TParam3 param3,
      TParam4 param4,
      TParam5 param5,
      CancellationToken cancellationToken)
    {
        var compiledQueryResult = await compiledQuery.Invoke(this.DbContext, param1, param2, param3, param4, param5, cancellationToken).ConfigureAwait(false);

        if (compiledQueryResult is null) return PrimitiveResult.Failure<TResult>(GenericPrimitiveReadRepositoryErrors.Null_DbResult_Error);

        return PrimitiveResult.Success(compiledQueryResult);
    }
}

