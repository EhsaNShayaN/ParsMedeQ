namespace ParsMedeq.Domain.Types.ProductTypes;

/// <summary>
/// تعداد رای محصول
/// </summary>
public readonly partial record struct ProductRatingsCountType :
    ITaxPayerSystemType<int>,
    IDbType<int>
{

    public readonly int Id { get; }

    private ProductRatingsCountType(int id)
    {
        this.Id = id;
    }
    public int GetValue() => this.Id;
    public int GetDbValue() => this.Id;
}
