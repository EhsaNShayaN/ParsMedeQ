using ParsMedeq.Domain.Helpers;

namespace ParsMedeq.Domain.Types.ProductTypes;
/// <summary>
/// نام محصول
/// </summary>
public record ProductNameType : IDbType<string>
{
    public const int MAX_LENGTH = 255;

    const string ERROR_MESSAGE = "نام محصول اشتباه است";

    public readonly static ProductNameType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private ProductNameType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<ProductNameType> Create(string value) => CreateUnsafe(value).Validate();

    public static ProductNameType CreateUnsafe(string? value) => new(value ?? string.Empty);

    public PrimitiveResult<ProductNameType> Validate() =>
        this.Value.Length > MAX_LENGTH
            ? DomainTypesHelper.CreateTypeErrorResult<ProductNameType>(ERROR_MESSAGE)
            : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetDbValue() => this.Value;

    public static ProductNameType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
