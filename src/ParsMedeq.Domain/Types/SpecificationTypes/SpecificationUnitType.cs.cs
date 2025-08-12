using ParsMedeq.Domain.Helpers;

namespace ParsMedeq.Domain.Types.SpecificationTypes;
/// <summary>
/// مقادیر مشخصه
/// </summary>
public record SpecificationDataType : IDbType<string>
{
    public const int MAX_LENGTH = 255;

    const string ERROR_MESSAGE = "مقادیر مشخصه اشتباه است";

    public readonly static SpecificationDataType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private SpecificationDataType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<SpecificationDataType> Create(string value) => CreateUnsafe(value).Validate();

    public static SpecificationDataType CreateUnsafe(string? value) => new(value ?? string.Empty);

    public PrimitiveResult<SpecificationDataType> Validate() =>
        this.Value.Length > MAX_LENGTH
            ? DomainTypesHelper.CreateTypeErrorResult<SpecificationDataType>(ERROR_MESSAGE)
            : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetDbValue() => this.Value;

    public static SpecificationDataType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
