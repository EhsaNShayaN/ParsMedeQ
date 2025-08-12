using ParsMedeq.Domain.Helpers;

namespace ParsMedeq.Domain.Types.BrandTypes;
/// <summary>
/// نام برند
/// </summary>
public record BrandNameType : IDbType<string>
{
    public const int MAX_LENGTH = 255;

    const string ERROR_MESSAGE = "نام برند اشتباه است";

    public readonly static BrandNameType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private BrandNameType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<BrandNameType> Create(string value) => CreateUnsafe(value).Validate();

    public static BrandNameType CreateUnsafe(string? value) => new(value ?? string.Empty);

    public PrimitiveResult<BrandNameType> Validate() =>
        this.Value.Length > MAX_LENGTH
            ? DomainTypesHelper.CreateTypeErrorResult<BrandNameType>(ERROR_MESSAGE)
            : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetDbValue() => this.Value;

    public static BrandNameType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
