using ParsMedeq.Domain.Helpers;

namespace ParsMedeq.Domain.Types.ProductTypes;
/// <summary>
/// ویدئو محصول
/// </summary>
public record ProductVideosType : IDbType<string>
{
    public const int MAX_LENGTH = 255;

    const string ERROR_MESSAGE = "ویدئو محصول اشتباه است";

    public readonly static ProductVideosType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private ProductVideosType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<ProductVideosType> Create(string value) => CreateUnsafe(value).Validate();

    public static ProductVideosType CreateUnsafe(string? value) => new(value ?? string.Empty);

    public PrimitiveResult<ProductVideosType> Validate() =>
        this.Value.Length > MAX_LENGTH
            ? DomainTypesHelper.CreateTypeErrorResult<ProductVideosType>(ERROR_MESSAGE)
            : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetDbValue() => this.Value;

    public static ProductVideosType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
