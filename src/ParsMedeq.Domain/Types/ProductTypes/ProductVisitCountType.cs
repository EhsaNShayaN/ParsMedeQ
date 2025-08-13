namespace ParsMedeQ.Domain.Types.ProductTypes;

/// <summary>
/// تعداد نمایش محصول
/// </summary>
public readonly partial record struct ProductVisitCountType :
    ITaxPayerSystemType<int>,
    IDbType<int>
{

    public readonly int Id { get; }

    private ProductVisitCountType(int id)
    {
        this.Id = id;
    }
    public int GetValue() => this.Id;
    public int GetDbValue() => this.Id;
}
