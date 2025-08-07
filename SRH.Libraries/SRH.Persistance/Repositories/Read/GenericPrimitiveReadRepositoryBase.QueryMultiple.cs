using SRH.Persistance.Models;
using static Dapper.SqlMapper;

namespace SRH.Persistance.Repositories.Read;
public abstract partial class GenericPrimitiveReadRepositoryBase<TDbContext>
{
    public readonly static PrimitiveError Null_QueryMultiple_Error = PrimitiveError.Create("", "null query multiple result");
    public async ValueTask<PrimitiveResult<MultipleReader<T1, T2>>> QueryMultipleAsync<T1, T2>(
        CommandDefinition command,
        Func<GridReader, Task<T1>> mapper1,
        Func<GridReader, Task<T2>> mapper2)
    {
        var dbResult = await this.GetDbConnection().QueryMultipleAsync(command).ConfigureAwait(false);

        if (dbResult is null) return PrimitiveResult.Failure<MultipleReader<T1, T2>>(Null_QueryMultiple_Error);

        var result = new MultipleReader<T1, T2>()
        {
            Item1 = await mapper1(dbResult).ConfigureAwait(false),
            Item2 = await mapper2(dbResult).ConfigureAwait(false)
        };
        return PrimitiveResult.Success(result);
    }

    public async ValueTask<PrimitiveResult<MultipleReader<T1, T2, T3>>> QueryMultipleAsync<T1, T2, T3>(
        CommandDefinition command,
        Func<GridReader, Task<T1>> mapper1,
        Func<GridReader, Task<T2>> mapper2,
        Func<GridReader, Task<T3>> mapper3)
    {
        var dbResult = await this.GetDbConnection().QueryMultipleAsync(command).ConfigureAwait(false);

        if (dbResult is null) return PrimitiveResult.Failure<MultipleReader<T1, T2, T3>>(Null_QueryMultiple_Error);

        var result = new MultipleReader<T1, T2, T3>()
        {
            Item1 = await mapper1(dbResult).ConfigureAwait(false),
            Item2 = await mapper2(dbResult).ConfigureAwait(false),
            Item3 = await mapper3(dbResult).ConfigureAwait(false)
        };
        return PrimitiveResult.Success(result);
    }

    public async ValueTask<PrimitiveResult<MultipleReader<T1, T2, T3, T4>>> QueryMultipleAsync<T1, T2, T3, T4>(
        CommandDefinition command,
        Func<GridReader, Task<T1>> mapper1,
        Func<GridReader, Task<T2>> mapper2,
        Func<GridReader, Task<T3>> mapper3,
        Func<GridReader, Task<T4>> mapper4)
    {
        var dbResult = await this.GetDbConnection().QueryMultipleAsync(command).ConfigureAwait(false);

        if (dbResult is null) return PrimitiveResult.Failure<MultipleReader<T1, T2, T3, T4>>(Null_QueryMultiple_Error);

        var result = new MultipleReader<T1, T2, T3, T4>()
        {
            Item1 = await mapper1(dbResult).ConfigureAwait(false),
            Item2 = await mapper2(dbResult).ConfigureAwait(false),
            Item3 = await mapper3(dbResult).ConfigureAwait(false),
            Item4 = await mapper4(dbResult).ConfigureAwait(false)
        };
        return PrimitiveResult.Success(result);
    }

    public async ValueTask<PrimitiveResult<MultipleReader<T1, T2, T3, T4, T5>>> QueryMultipleAsync<T1, T2, T3, T4, T5>(
        CommandDefinition command,
        Func<GridReader, Task<T1>> mapper1,
        Func<GridReader, Task<T2>> mapper2,
        Func<GridReader, Task<T3>> mapper3,
        Func<GridReader, Task<T4>> mapper4,
        Func<GridReader, Task<T5>> mapper5)
    {
        var dbResult = await this.GetDbConnection().QueryMultipleAsync(command).ConfigureAwait(false);

        if (dbResult is null) return PrimitiveResult.Failure<MultipleReader<T1, T2, T3, T4, T5>>(Null_QueryMultiple_Error);

        var result = new MultipleReader<T1, T2, T3, T4, T5>()
        {
            Item1 = await mapper1(dbResult).ConfigureAwait(false),
            Item2 = await mapper2(dbResult).ConfigureAwait(false),
            Item3 = await mapper3(dbResult).ConfigureAwait(false),
            Item4 = await mapper4(dbResult).ConfigureAwait(false),
            Item5 = await mapper5(dbResult).ConfigureAwait(false)
        };
        return PrimitiveResult.Success(result);
    }

    public async ValueTask<PrimitiveResult<MultipleReader<T1, T2, T3, T4, T5, T6>>> QueryMultipleAsync<T1, T2, T3, T4, T5, T6>(
        CommandDefinition command,
        Func<GridReader, Task<T1>> mapper1,
        Func<GridReader, Task<T2>> mapper2,
        Func<GridReader, Task<T3>> mapper3,
        Func<GridReader, Task<T4>> mapper4,
        Func<GridReader, Task<T5>> mapper5,
        Func<GridReader, Task<T6>> mapper6)
    {
        var dbResult = await this.GetDbConnection().QueryMultipleAsync(command).ConfigureAwait(false);

        if (dbResult is null) return PrimitiveResult.Failure<MultipleReader<T1, T2, T3, T4, T5, T6>>(Null_QueryMultiple_Error);

        var result = new MultipleReader<T1, T2, T3, T4, T5, T6>()
        {
            Item1 = await mapper1(dbResult).ConfigureAwait(false),
            Item2 = await mapper2(dbResult).ConfigureAwait(false),
            Item3 = await mapper3(dbResult).ConfigureAwait(false),
            Item4 = await mapper4(dbResult).ConfigureAwait(false),
            Item5 = await mapper5(dbResult).ConfigureAwait(false),
            Item6 = await mapper6(dbResult).ConfigureAwait(false)
        };
        return PrimitiveResult.Success(result);
    }
}
