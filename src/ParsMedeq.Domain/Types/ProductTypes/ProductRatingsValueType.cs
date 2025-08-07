namespace EShop.Domain.Types.ProductTypes;

/// <summary>
/// ارزش رای محصول
/// </summary>
public readonly partial record struct ProductRatingsValueType :
    ITaxPayerSystemType<int>,
    IDbType<int>
{

    public readonly int Id { get; }

    private ProductRatingsValueType(int id)
    {
        this.Id = id;
    }
    public int GetValue() => this.Id;
    public int GetDbValue() => this.Id;
}
