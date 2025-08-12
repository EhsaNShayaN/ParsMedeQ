namespace ParsMedeq.Domain.Types.ProductVariantTypes;
/// <summary>
/// شناسه نوع محصول
/// </summary>
/// <param name="Value"></param>
public record ProductVariantIdType : IDbType<int>
{
    public readonly static ProductVariantIdType Empty = new(0);
    public int Value { get; }
    public ProductVariantIdType(int value) => this.Value = value;
    public int GetDbValue() => this.Value;
    public static ProductVariantIdType FromDb(int value) => new(value);
}