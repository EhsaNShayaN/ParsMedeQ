using ParsMedeQ.Domain.Helpers;

namespace ParsMedeQ.Domain.Types.CategoryTypes;
/// <summary>
/// عنوان برند
/// </summary>
public record CategoryTitleType : IDbType<string>
{
    public const int MAX_LENGTH = 255;

    const string ERROR_MESSAGE = "عنوان برند اشتباه است";

    public readonly static CategoryTitleType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private CategoryTitleType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<CategoryTitleType> Create(string value) => CreateUnsafe(value).Validate();

    public static CategoryTitleType CreateUnsafe(string? value) => new(value ?? string.Empty);

    public PrimitiveResult<CategoryTitleType> Validate() =>
        this.Value.Length > MAX_LENGTH
            ? DomainTypesHelper.CreateTypeErrorResult<CategoryTitleType>(ERROR_MESSAGE)
            : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetDbValue() => this.Value;

    public static CategoryTitleType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
