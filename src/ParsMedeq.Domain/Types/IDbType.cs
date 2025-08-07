namespace EShop.Domain.Types;
public interface IDbType
{
    //string ColumnType { get; }
}
public interface IDbType<TValue> : IDbType
{
    TValue GetDbValue();
}
public interface ITaxPayerSystemType<TValue>
{
    TValue GetValue();
}
