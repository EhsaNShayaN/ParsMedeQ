using Microsoft.Data.SqlClient;

namespace SRH.Persistance.Repositories.Write;
public abstract partial class GenericPrimitiveWriteRepositoryBase<TDbContext>
{
    public async ValueTask<PrimitiveResult> BulkInsert(
        SqlConnection connection,
        SqlTransaction? sqlTransaction,
        DataTable datatable,
        string tablename,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!connection.State.Equals(ConnectionState.Open))
            {
                await connection.OpenAsync(cancellationToken);
            }

            using var sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, sqlTransaction);

            sqlBulkCopy.DestinationTableName = tablename;
            sqlBulkCopy.EnableStreaming = true;
            sqlBulkCopy.BatchSize = 5000;
            await sqlBulkCopy.WriteToServerAsync(datatable, cancellationToken);

            return PrimitiveResult.Success();
        }
        catch (Exception ex)
        {
            return PrimitiveResult.Failure("Unhandled.Error", ex.Message);
        }
    }

    public ValueTask<PrimitiveResult> BulkInsert(DataTable datatable, string tablename, CancellationToken cancellationToken)
    {
        return this.BulkInsert((SqlConnection)this.GetDbConnection(), null, datatable, tablename, cancellationToken);
    }
}
