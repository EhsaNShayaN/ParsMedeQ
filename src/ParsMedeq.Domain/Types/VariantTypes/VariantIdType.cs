namespace ParsMedeQ.Domain.Types.VariantTypes;
/// <summary>
/// شناسه رنگ یا سایز
/// </summary>
/// <param name="Value"></param>
public record VariantIdType : IDbType<int>
{
    public readonly static VariantIdType Empty = new(0);
    public int Value { get; }
    public VariantIdType(int value) => this.Value = value;
    public int GetDbValue() => this.Value;
    public static VariantIdType FromDb(int value) => new(value);
}