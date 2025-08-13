namespace ParsMedeQ.Domain.Types.ProfileTypes;
/// <summary>
/// شناسه برند
/// </summary>
/// <param name="Value"></param>
public record ProfileIdType : IDbType<int>
{
    public readonly static ProfileIdType Empty = new(0);
    public int Value { get; }
    public ProfileIdType(int value) => this.Value = value;
    public int GetDbValue() => this.Value;
    public static ProfileIdType FromDb(int value) => new(value);
}