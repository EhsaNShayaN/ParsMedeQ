namespace EShop.Domain.Types.SpecificationTypes;
/// <summary>
/// شناسه مشخصه
/// </summary>
/// <param name="Value"></param>
public record SpecificationIdType : IDbType<int>
{
    public readonly static SpecificationIdType Empty = new(0);
    public int Value { get; }
    public SpecificationIdType(int value) => this.Value = value;
    public int GetDbValue() => this.Value;
    public static SpecificationIdType FromDb(int value) => new(value);
}