using ParsMedeQ.Domain.Helpers;

namespace ParsMedeQ.Domain.Types.CategoryTypes;
/// <summary>
/// نام برند
/// </summary>
public record CategoryNameType : IDbType<string>
{
    public const int MAX_LENGTH = 255;

    const string ERROR_MESSAGE = "نام برند اشتباه است";

    public readonly static CategoryNameType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private CategoryNameType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<CategoryNameType> Create(string value) => CreateUnsafe(value).Validate();

    public static CategoryNameType CreateUnsafe(string? value) => new(value ?? string.Empty);

    public PrimitiveResult<CategoryNameType> Validate() =>
        this.Value.Length > MAX_LENGTH
            ? DomainTypesHelper.CreateTypeErrorResult<CategoryNameType>(ERROR_MESSAGE)
            : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetDbValue() => this.Value;

    public static CategoryNameType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
