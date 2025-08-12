namespace ParsMedeq.Domain.Types.ProductTypes;

/// <summary>
/// تعداد محصول
/// </summary>
public readonly partial record struct ProductQuantityType :
    ITaxPayerSystemType<int>,
    IDbType<int>
{

    public readonly int Id { get; }

    private ProductQuantityType(int id)
    {
        this.Id = id;
    }
    public int GetValue() => this.Id;
    public int GetDbValue() => this.Id;
}
