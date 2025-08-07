using EShop.Domain.Helpers;

namespace EShop.Domain.Types.ProductTypes;
/// <summary>
/// نام انگلیسی محصول
/// </summary>
public record ProductTitleType : IDbType<string>
{
    public const int MAX_LENGTH = 255;

    const string ERROR_MESSAGE = "نام انگلیسی محصول اشتباه است";

    public readonly static ProductTitleType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private ProductTitleType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<ProductTitleType> Create(string value) => CreateUnsafe(value).Validate();

    public static ProductTitleType CreateUnsafe(string? value) => new(value ?? string.Empty);

    public PrimitiveResult<ProductTitleType> Validate() =>
        this.Value.Length > MAX_LENGTH
            ? DomainTypesHelper.CreateTypeErrorResult<ProductTitleType>(ERROR_MESSAGE)
            : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetDbValue() => this.Value;

    public static ProductTitleType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
