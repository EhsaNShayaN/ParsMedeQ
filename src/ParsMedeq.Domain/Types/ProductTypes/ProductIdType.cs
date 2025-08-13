namespace ParsMedeQ.Domain.Types.ProductTypes;
/// <summary>
/// شناسه محصول
/// </summary>
/// <param name="Value"></param>
public record ProductIdType : IDbType<int>
{
    public readonly static ProductIdType Empty = new(0);
    public int Value { get; }
    public ProductIdType(int value) => this.Value = value;
    public int GetDbValue() => this.Value;
    public static ProductIdType FromDb(int value) => new(value);
}