using SRH.Persistance.Extensions;
using System.Runtime.CompilerServices;
using static Dapper.SqlMapper;

namespace SRH.Persistance.Repositories.Read;
public abstract partial class GenericPrimitiveReadRepositoryBase<TDbContext>
{
    protected DbConnection GetDbConnection() => this._dbContext.Database.GetDbConnection();

    public virtual async ValueTask<PrimitiveResult<IEnumerable<T>>> QueryOrEmptyAsync<T>(CommandDefinition command) =>
        PrimitiveMaybe.From(await this.GetDbConnection().QueryAsync<T>(command).ConfigureAwait(false))
            .Map(data => PrimitiveResult.Success(data ?? Enumerable.Empty<T>()))
            .GetOr(PrimitiveResult.Success(Enumerable.Empty<T>()));

    public virtual async ValueTask<PrimitiveResult<T>> QueryFirstOrDefault<T>(CommandDefinition command, T defaultValue) =>
      PrimitiveMaybe.From(await this.GetDbConnection().QueryFirstOrDefaultAsync<T>(command).ConfigureAwait(false))
          .Map(data => PrimitiveResult.Success(data))
          .GetOr(PrimitiveResult.Success(defaultValue));

    public virtual async ValueTask<PrimitiveResult<T>> QueryFirst<T>(CommandDefinition command)
    {
        var dbResult = await this.GetDbConnection().QueryFirstOrDefaultAsync<T>(command).ConfigureAwait(false);
        return PrimitiveMaybe.From(dbResult)
          .Map(data => PrimitiveResult.Success(data))
          .GetOr(PrimitiveResult.Failure<T>("", "An error occured"));
    }

    public virtual async IAsyncEnumerable<T> ExecuteReaderAsync<T>(CommandDefinition command, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var connection = this.GetDbConnection();
        using var reader = await connection.ExecuteReaderAsync(command);

        if (reader is null) yield break;
        var rowParser = reader.GetRowParser<T>();

        while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
        {
            if (cancellationToken.IsCancellationRequested) yield break;
            yield return rowParser(reader);
        }
    }
    public virtual IEnumerable<T> ExecuteReader<T>(CommandDefinition command)
    {
        var connection = this.GetDbConnection();
        using var reader = connection.ExecuteReader(command);

        if (reader is null) yield break;
        var rowParser = reader.GetRowParser<T>();

        while (reader.Read())
        {
            yield return rowParser(reader);
        }
    }

    public virtual async ValueTask<PrimitiveResult> QueryDefaultProcedureStatusAsync(CommandDefinition command, int successResult = 1) =>
        await this.QueryFirst<DefaultProcedureStatusResult>(command)
            .MapDefaultProcedureStatusResult();
}
