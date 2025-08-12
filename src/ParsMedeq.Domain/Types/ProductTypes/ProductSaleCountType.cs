namespace ParsMedeq.Domain.Types.ProductTypes;

/// <summary>
/// تعداد فروش محصول
/// </summary>
public readonly partial record struct ProductSaleCountType :
    ITaxPayerSystemType<int>,
    IDbType<int>
{

    public readonly int Id { get; }

    private ProductSaleCountType(int id)
    {
        this.Id = id;
    }
    public int GetValue() => this.Id;
    public int GetDbValue() => this.Id;
}
