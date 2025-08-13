using ParsMedeQ.Domain.Helpers;

namespace ParsMedeQ.Domain.Types.ProductTypes;
/// <summary>
/// تصویر محصول
/// </summary>
public record ProductImagesType : IDbType<string>
{
    public const int MAX_LENGTH = 255;

    const string ERROR_MESSAGE = "تصویر محصول اشتباه است";

    public readonly static ProductImagesType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private ProductImagesType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<ProductImagesType> Create(string value) => CreateUnsafe(value).Validate();

    public static ProductImagesType CreateUnsafe(string? value) => new(value ?? string.Empty);

    public PrimitiveResult<ProductImagesType> Validate() =>
        this.Value.Length > MAX_LENGTH
            ? DomainTypesHelper.CreateTypeErrorResult<ProductImagesType>(ERROR_MESSAGE)
            : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetDbValue() => this.Value;

    public static ProductImagesType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
