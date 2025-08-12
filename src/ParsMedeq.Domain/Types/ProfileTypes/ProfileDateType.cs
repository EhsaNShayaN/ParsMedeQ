namespace ParsMedeq.Domain.Types.ProfileTypes;

/// <summary>
/// تاریخ انقضاء تخفیف محصمول
/// </summary>
public readonly partial record struct ProfileDateType :
    ITaxPayerSystemType<DateTime>,
    IDbType<DateTime>
{

    public readonly DateTime Id { get; }

    private ProfileDateType(DateTime id)
    {
        this.Id = id;
    }
    public DateTime GetValue() => this.Id;
    public DateTime GetDbValue() => this.Id;
}
