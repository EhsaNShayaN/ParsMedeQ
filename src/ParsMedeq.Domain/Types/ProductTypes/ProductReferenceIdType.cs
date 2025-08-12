namespace ParsMedeq.Domain.Types.ProductTypes;

/// <summary>
/// تعداد فروش محصول
/// </summary>
public readonly partial record struct ProductReferenceIdType :
    ITaxPayerSystemType<int>,
    IDbType<int>
{

    public readonly int Id { get; }

    private ProductReferenceIdType(int id)
    {
        this.Id = id;
    }
    public int GetValue() => this.Id;
    public int GetDbValue() => this.Id;
}
