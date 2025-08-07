namespace EShop.Domain.Types.ProductVariantTypes;

/// <summary>
/// تعداد نمایش محصول
/// </summary>
public readonly partial record struct ProductVariantQuantityType :
    ITaxPayerSystemType<int>,
    IDbType<int>
{

    public readonly int Id { get; }

    private ProductVariantQuantityType(int id)
    {
        this.Id = id;
    }
    public int GetValue() => this.Id;
    public int GetDbValue() => this.Id;
}
