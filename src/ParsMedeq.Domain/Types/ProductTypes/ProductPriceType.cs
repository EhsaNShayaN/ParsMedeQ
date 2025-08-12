namespace ParsMedeq.Domain.Types.ProductTypes;

/// <summary>
/// قیمت محصول
/// </summary>
public readonly partial record struct ProductPriceType :
    ITaxPayerSystemType<int>,
    IDbType<int>
{

    public readonly int Id { get; }

    private ProductPriceType(int id)
    {
        this.Id = id;
    }
    public int GetValue() => this.Id;
    public int GetDbValue() => this.Id;
}
