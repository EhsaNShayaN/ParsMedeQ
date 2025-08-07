using System.Text.RegularExpressions;

namespace SRH.ValueObjects.NationalCode;

public readonly record struct NationalCode : IEquatable<string>
{
    private readonly static string[] InvalidNationalCodes =
    {
        "0000000000",
        "1111111111",
        "2222222222",
        "3333333333",
        "4444444444",
        "5555555555",
        "6666666666",
        "7777777777",
        "8888888888",
        "9999999999"
    };

    private static readonly Regex NationalCodeFormatRegex = new Regex(@"^\d{10}$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);

    public readonly static NationalCode Empty = new(null);

    public static bool IsEmpty(NationalCode src) => src.Equals(Empty);

    private readonly string? _value;

    public readonly string Value => _value ?? string.Empty;

    public NationalCode() : this(string.Empty) { }
    private NationalCode(string? value) => _value = value;

    public static ValueTask<PrimitiveResult<NationalCode>> Create(string value) =>
        PrimitiveResult.Success(Sanitize(value))
                .Bind(IsValid)
                .Map(_ => new NationalCode(_));

    public static NationalCode FromDb(string value) => HasValue(value) ? new(value) : Empty;

    public static ValueTask<PrimitiveResult<string>> IsValid(string value)
    {
        var result = Validate(value);
        if (result.IsFailure) return ValueTask.FromResult(PrimitiveResult.Failure<string>(result.Errors));
        return ValueTask.FromResult(PrimitiveResult.Success(value));
    }
    public static PrimitiveResult Validate(string value)
    {
        if (!NationalCodeFormatRegex.IsMatch(value)) return PrimitiveResult.Failure(ValueObjectErrors.NationalCodeIsInvalidError);

        if (InvalidNationalCodes.Contains(value)) return PrimitiveResult.Failure(ValueObjectErrors.NationalCodeIsInvalidError);

        var check = Convert.ToInt32(value.Substring(9, 1));
        var sum = Enumerable.Range(0, 9)
            .Select(x => Convert.ToInt32(value.Substring(x, 1)) * (10 - x))
            .Sum() % 11;
        if (
            sum < 2 && check == sum ||
            sum >= 2 && check + sum == 11) return PrimitiveResult.Success();

        return PrimitiveResult.Failure(ValueObjectErrors.NationalCodeIsInvalidError);
    }
    public static implicit operator string(NationalCode src) => src.Value;

    public override string ToString() => Value;

    private static string Sanitize(string value) => value.Trim();

    private static bool HasValue(string? str) => str is not null
        && !string.IsNullOrWhiteSpace(str ?? string.Empty)
        && !string.IsNullOrEmpty(str ?? string.Empty);

    public bool Equals(string? other) =>
        other is null
        ? false
        : Value.Equals(other!, StringComparison.InvariantCultureIgnoreCase);
}
