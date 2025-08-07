namespace SRH.ValueObjects.BirthDate;

public readonly record struct BirthdateInfo :
    IEquatable<DateOnly>,
    IEquatable<DateTime>,
    IEquatable<DateTimeOffset>
{
    public readonly static BirthdateInfo Empty = new BirthdateInfo(DateOnly.MinValue);

    public static bool IsEmpty(BirthdateInfo src) => src.Equals(Empty);


    public readonly DateOnly Value { get; }

    public BirthdateInfo() : this(DateOnly.MinValue) { }
    private BirthdateInfo(DateOnly value) => Value = value;

    public static ValueTask<PrimitiveResult<BirthdateInfo>> Create(DateOnly value) =>
        PrimitiveResult.Success(value)
            .Ensure(_ => !_.Equals(DateOnly.MinValue), ValueObjectErrors.BirthdateValueError)
            .Ensure(_ => !_.Equals(DateOnly.MaxValue), ValueObjectErrors.BirthdateValueError)
            .Ensure(_ => DateOnly.FromDateTime(DateTime.Today).DayNumber >= value.DayNumber, ValueObjectErrors.BirthdateValueError)
            .Map(_ => new BirthdateInfo(value));
    public static ValueTask<PrimitiveResult<BirthdateInfo>> Create(DateTime value) => Create(DateOnly.FromDateTime(value));
    public static ValueTask<PrimitiveResult<BirthdateInfo>> Create(DateTimeOffset value) => Create(DateOnly.FromDateTime(value.DateTime));
    public static ValueTask<PrimitiveResult<BirthdateInfo>> Create(string value) =>
        PrimitiveResult.Success(value)
            .Map(_ =>
            {
                if (DateTime.TryParse(value, out var result)) return result;
                return DateTime.MinValue;
            })
            .Map(Create);

    public static BirthdateInfo FromDb(DateTime dbValue) =>
        dbValue.Equals(DateTime.MinValue) || dbValue.Equals(DateTime.MaxValue)
            ? Empty
            : new BirthdateInfo(DateOnly.FromDateTime(dbValue));
    public static BirthdateInfo FromDb(DateOnly dbValue) => FromDb(new DateTime(dbValue, TimeOnly.MinValue));

    public ValueTask<PrimitiveResult<AgeInfo>> GetAge(DateOnly from) => AgeInfo.Create(this, from);
    public ValueTask<PrimitiveResult<AgeInfo>> GetAge() => AgeInfo.Create(this, DateOnly.FromDateTime(DateTime.Today));

    public static implicit operator string(BirthdateInfo src) => src.ToString();

    public override string ToString() => Value.ToString("yyyy-MM-dd");

    public bool Equals(DateOnly other) => Value.Equals(other);

    public bool Equals(DateTime other) => Value.Equals(DateOnly.FromDateTime(other));

    public bool Equals(DateTimeOffset other) => Value.Equals(DateOnly.FromDateTime(other.DateTime));

    public override int GetHashCode() => Value.GetHashCode();

}
