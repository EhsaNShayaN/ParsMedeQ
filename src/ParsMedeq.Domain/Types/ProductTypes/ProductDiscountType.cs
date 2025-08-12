namespace ParsMedeq.Domain.Types.ProductTypes;

/// <summary>
/// تخفیف محصول
/// </summary>
public readonly partial record struct ProductDiscountType :
    ITaxPayerSystemType<int>,
    IDbType<int>
{

    public readonly int Id { get; }

    private ProductDiscountType(int id)
    {
        this.Id = id;
    }
    public int GetValue() => this.Id;
    public int GetDbValue() => this.Id;
}
