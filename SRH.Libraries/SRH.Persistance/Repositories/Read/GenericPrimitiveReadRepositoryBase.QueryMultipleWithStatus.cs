using SRH.Persistance.Models;
using static Dapper.SqlMapper;

namespace SRH.Persistance.Repositories.Read;
public abstract partial class GenericPrimitiveReadRepositoryBase<TDbContext>
{
    public async ValueTask<PrimitiveResult<MultipleReader<DefaultProcedureStatusResult, T?>>> QueryMultipleWithStatusAsync<T>(
       CommandDefinition command,
       Func<GridReader, Task<T>> mapper1,
       int successResult = 1)
    {
        var dbResult = await this.GetDbConnection().QueryMultipleAsync(command).ConfigureAwait(false);

        if (dbResult is null) return PrimitiveResult.Failure<MultipleReader<DefaultProcedureStatusResult, T?>>(Null_QueryMultiple_Error);

        var status = await dbResult.ReadFirstAsync<DefaultProcedureStatusResult>().ConfigureAwait(false);

        if (status is null) return PrimitiveResult.Failure<MultipleReader<DefaultProcedureStatusResult, T?>>(Null_QueryMultiple_Error);

        if (!status.ResultStatus.Equals(successResult)) return PrimitiveResult.Failure<MultipleReader<DefaultProcedureStatusResult, T?>>(
            PrimitiveError.Create("Status.Error", status.Message));

        var result = new MultipleReader<DefaultProcedureStatusResult, T?>()
        {
            Item1 = status,
            Item2 = await mapper1(dbResult).ConfigureAwait(false)
        };
        return PrimitiveResult.Success(result);
    }

    public async ValueTask<PrimitiveResult<MultipleReader<DefaultProcedureStatusResult, T1?, T2?>>> QueryMultipleWithStatusAsync<T1, T2>(
       CommandDefinition command,
       Func<GridReader, Task<T1>> mapper1,
       Func<GridReader, Task<T2>> mapper2,
       int successResult = 1)
    {
        var dbResult = await this.GetDbConnection().QueryMultipleAsync(command).ConfigureAwait(false);

        if (dbResult is null) return PrimitiveResult.Failure<MultipleReader<DefaultProcedureStatusResult, T1?, T2?>>(Null_QueryMultiple_Error);

        var status = await dbResult.ReadFirstAsync<DefaultProcedureStatusResult>().ConfigureAwait(false);

        if (status is null) return PrimitiveResult.Failure<MultipleReader<DefaultProcedureStatusResult, T1?, T2?>>(Null_QueryMultiple_Error);

        if (!status.ResultStatus.Equals(successResult)) return PrimitiveResult.Failure<MultipleReader<DefaultProcedureStatusResult, T1?, T2?>>(
            PrimitiveError.Create("Status.Error", status.Message));

        var result = new MultipleReader<DefaultProcedureStatusResult, T1?, T2?>()
        {
            Item1 = status,
            Item2 = await mapper1(dbResult).ConfigureAwait(false),
            Item3 = await mapper2(dbResult).ConfigureAwait(false)
        };
        return PrimitiveResult.Success(result);
    }

    public async ValueTask<PrimitiveResult<MultipleReader<DefaultProcedureStatusResult, T1?, T2?, T3?>>> QueryMultipleWithStatusAsync<T1, T2, T3>(
      CommandDefinition command,
      Func<GridReader, Task<T1>> mapper1,
      Func<GridReader, Task<T2>> mapper2,
      Func<GridReader, Task<T3>> mapper3,
      int successResult = 1)
    {
        var dbResult = await this.GetDbConnection().QueryMultipleAsync(command).ConfigureAwait(false);

        if (dbResult is null) return PrimitiveResult.Failure<MultipleReader<DefaultProcedureStatusResult, T1?, T2?, T3?>>(Null_QueryMultiple_Error);

        var status = await dbResult.ReadFirstAsync<DefaultProcedureStatusResult>().ConfigureAwait(false);

        if (status is null) return PrimitiveResult.Failure<MultipleReader<DefaultProcedureStatusResult, T1?, T2?, T3?>>(Null_QueryMultiple_Error);

        if (!status.ResultStatus.Equals(successResult)) return PrimitiveResult.Failure<MultipleReader<DefaultProcedureStatusResult, T1?, T2?, T3?>>(
            PrimitiveError.Create("Status.Error", status.Message));

        var result = new MultipleReader<DefaultProcedureStatusResult, T1?, T2?, T3?>()
        {
            Item1 = status,
            Item2 = await mapper1(dbResult).ConfigureAwait(false),
            Item3 = await mapper2(dbResult).ConfigureAwait(false),
            Item4 = await mapper3(dbResult).ConfigureAwait(false)
        };
        return PrimitiveResult.Success(result);
    }

    public async ValueTask<PrimitiveResult<MultipleReader<DefaultProcedureStatusResult, T1?, T2?, T3?, T4?>>> QueryMultipleWithStatusAsync<T1, T2, T3, T4>(
     CommandDefinition command,
     Func<GridReader, Task<T1>> mapper1,
     Func<GridReader, Task<T2>> mapper2,
     Func<GridReader, Task<T3>> mapper3,
     Func<GridReader, Task<T4>> mapper4,
     int successResult = 1)
    {
        var dbResult = await this.GetDbConnection().QueryMultipleAsync(command).ConfigureAwait(false);

        if (dbResult is null) return PrimitiveResult.Failure<MultipleReader<DefaultProcedureStatusResult, T1?, T2?, T3?, T4?>>(Null_QueryMultiple_Error);

        var status = await dbResult.ReadFirstAsync<DefaultProcedureStatusResult>().ConfigureAwait(false);

        if (status is null) return PrimitiveResult.Failure<MultipleReader<DefaultProcedureStatusResult, T1?, T2?, T3?, T4?>>(Null_QueryMultiple_Error);

        if (!status.ResultStatus.Equals(successResult)) return PrimitiveResult.Failure<MultipleReader<DefaultProcedureStatusResult, T1?, T2?, T3?, T4?>>(
            PrimitiveError.Create("Status.Error", status.Message));

        var result = new MultipleReader<DefaultProcedureStatusResult, T1?, T2?, T3?, T4?>()
        {
            Item1 = status,
            Item2 = await mapper1(dbResult).ConfigureAwait(false),
            Item3 = await mapper2(dbResult).ConfigureAwait(false),
            Item4 = await mapper3(dbResult).ConfigureAwait(false),
            Item5 = await mapper4(dbResult).ConfigureAwait(false)
        };
        return PrimitiveResult.Success(result);
    }
    public async ValueTask<PrimitiveResult<MultipleReader<DefaultProcedureStatusResult, T1?, T2?, T3?, T4?, T5?>>> QueryMultipleWithStatusAsync<T1, T2, T3, T4, T5>(
    CommandDefinition command,
    Func<GridReader, Task<T1>> mapper1,
    Func<GridReader, Task<T2>> mapper2,
    Func<GridReader, Task<T3>> mapper3,
    Func<GridReader, Task<T4>> mapper4,
    Func<GridReader, Task<T5>> mapper5,
    int successResult = 1)
    {
        var dbResult = await this.GetDbConnection().QueryMultipleAsync(command).ConfigureAwait(false);

        if (dbResult is null) return PrimitiveResult.Failure<MultipleReader<DefaultProcedureStatusResult, T1?, T2?, T3?, T4?, T5?>>(Null_QueryMultiple_Error);

        var status = await dbResult.ReadFirstAsync<DefaultProcedureStatusResult>().ConfigureAwait(false);

        if (status is null) return PrimitiveResult.Failure<MultipleReader<DefaultProcedureStatusResult, T1?, T2?, T3?, T4?, T5?>>(Null_QueryMultiple_Error);

        if (!status.ResultStatus.Equals(successResult)) return PrimitiveResult.Failure<MultipleReader<DefaultProcedureStatusResult, T1?, T2?, T3?, T4?, T5?>>(
            PrimitiveError.Create("Status.Error", status.Message));

        var result = new MultipleReader<DefaultProcedureStatusResult, T1?, T2?, T3?, T4?, T5?>()
        {
            Item1 = status,
            Item2 = await mapper1(dbResult).ConfigureAwait(false),
            Item3 = await mapper2(dbResult).ConfigureAwait(false),
            Item4 = await mapper3(dbResult).ConfigureAwait(false),
            Item5 = await mapper4(dbResult).ConfigureAwait(false),
            Item6 = await mapper5(dbResult).ConfigureAwait(false)
        };
        return PrimitiveResult.Success(result);
    }
}