namespace ParsMedeQ.Domain.Types.CategoryTypes;
/// <summary>
/// شناسه برند
/// </summary>
/// <param name="Value"></param>
public record CategoryIdType : IDbType<int>
{
    public readonly static CategoryIdType Empty = new(0);
    public int Value { get; }
    public CategoryIdType(int value) => this.Value = value;
    public int GetDbValue() => this.Value;
    public static CategoryIdType FromDb(int value) => new(value);
}