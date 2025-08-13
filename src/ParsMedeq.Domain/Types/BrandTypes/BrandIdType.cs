namespace ParsMedeQ.Domain.Types.BrandTypes;
/// <summary>
/// شناسه برند
/// </summary>
/// <param name="Value"></param>
public record BrandIdType : IDbType<int>
{
    public readonly static BrandIdType Empty = new(0);
    public int Value { get; }
    public BrandIdType(int value) => this.Value = value;
    public int GetDbValue() => this.Value;
    public static BrandIdType FromDb(int value) => new(value);
}