using EShop.Domain.Helpers;

namespace EShop.Domain.Types.BrandTypes;
/// <summary>
/// عنوان برند
/// </summary>
public record BrandTitleType : IDbType<string>
{
    public const int MAX_LENGTH = 255;

    const string ERROR_MESSAGE = "عنوان برند اشتباه است";

    public readonly static BrandTitleType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private BrandTitleType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<BrandTitleType> Create(string value) => CreateUnsafe(value).Validate();

    public static BrandTitleType CreateUnsafe(string? value) => new(value ?? string.Empty);

    public PrimitiveResult<BrandTitleType> Validate() =>
        this.Value.Length > MAX_LENGTH
            ? DomainTypesHelper.CreateTypeErrorResult<BrandTitleType>(ERROR_MESSAGE)
            : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetDbValue() => this.Value;

    public static BrandTitleType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
