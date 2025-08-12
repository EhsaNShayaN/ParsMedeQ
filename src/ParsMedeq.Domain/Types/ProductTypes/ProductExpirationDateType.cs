namespace ParsMedeq.Domain.Types.ProductTypes;

/// <summary>
/// تاریخ انقضاء تخفیف محصمول
/// </summary>
public readonly partial record struct ProductExpirationDateType :
    ITaxPayerSystemType<DateTime>,
    IDbType<DateTime>
{

    public readonly DateTime Id { get; }

    private ProductExpirationDateType(DateTime id)
    {
        this.Id = id;
    }
    public DateTime GetValue() => this.Id;
    public DateTime GetDbValue() => this.Id;
}
