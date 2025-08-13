using ParsMedeQ.Domain.Helpers;

namespace ParsMedeQ.Domain.Types.UserId;
/// <summary>
/// شناسه کاربر
/// </summary>
/// <param name="Value"></param>
public record UserIdType : IDbType<int>
{
    public readonly static UserIdType Empty = new UserIdType(0);


    public int Value { get; }
    public UserIdType(int value) => this.Value = value;

    public int GetDbValue() => this.Value;
    public static UserIdType FromDb(int value) => new UserIdType(value);
    public static PrimitiveResult<UserIdType> Create(int value) => new UserIdType(value).Validate();

    public PrimitiveResult<UserIdType> Validate()
    {
        if (this.Value < 0) return PrimitiveResult.Failure<UserIdType>("", "شناسه کاربر اشتباه است");
        return this;
    }

    public string SerializeId => HashIdsHelper.Instance.Encode(this.Value);
    public static UserIdType Deserialize(string value) =>
         new UserIdType(HashIdsHelper.Instance.DecodeSingle(value));
}