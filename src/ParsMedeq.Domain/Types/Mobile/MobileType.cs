using ParsMedeq.Domain.Helpers;

namespace ParsMedeq.Domain.Types.Mobile;
/// <summary>
/// شماره موبایل
/// </summary>
public record MobileType :
    IDbType<string>
{
    static readonly Regex ValidationRegex = new Regex(@"^09[0-9]\d{8}$");

    public const int MAX_LENGTH = 15;
    const string ERROR_MESSAGE = "شماره موبایل اشتباه است";
    public readonly static MobileType Empty = new MobileType(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private MobileType(string value) => this.Value = value;

    public static PrimitiveResult<MobileType> Create(string value) =>
        CreateUnsafe(value)
        .Validate();

    public static MobileType CreateUnsafe(string value) =>
        new MobileType(
            SanitizeMobile(value) ?? string.Empty);

    public PrimitiveResult<MobileType> Validate()
    {
        if (this.IsDefault()) return PrimitiveResult.Success(this);

        if (!ValidationRegex.IsMatch(this.Value))
            return DomainTypesHelper.CreateTypeErrorResult<MobileType>(ERROR_MESSAGE);

        return this;
    }

    static string SanitizeMobile(string? val)
    {
        if (string.IsNullOrWhiteSpace(val)) return string.Empty;

        var result = val!.ConvertToGeorgianNumber().Trim();

        result = result.Replace("+", "").Replace("-", "").Replace(" ", "");
        if (result.StartsWith("0"))
            result = result.Remove(0, 1);
        if (result.StartsWith("0"))
            result = result.Remove(0, 1);
        if (result.StartsWith("98"))
            result = result.Remove(0, 2);

        return $"0{result}";

    }
    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || string.IsNullOrWhiteSpace(this.Value) || this.Equals(Empty);

    public string GetDbValue() => this.Value;
    public static MobileType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
