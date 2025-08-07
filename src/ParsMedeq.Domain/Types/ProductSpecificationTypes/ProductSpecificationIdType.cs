namespace EShop.Domain.Types.ProductSpecificationTypes;
/// <summary>
/// شناسه مشخصه ی محصول
/// </summary>
/// <param name="Value"></param>
public record ProductSpecificationIdType : IDbType<int>
{
    public readonly static ProductSpecificationIdType Empty = new(0);
    public int Value { get; }
    public ProductSpecificationIdType(int value) => this.Value = value;
    public int GetDbValue() => this.Value;
    public static ProductSpecificationIdType FromDb(int value) => new(value);
}