using ParsMedeQ.Domain.Helpers;

namespace ParsMedeQ.Domain.Types.SpecificationTypes;
/// <summary>
/// واحد مشخصه
/// </summary>
public record SpecificationUnitType : IDbType<string>
{
    public const int MAX_LENGTH = 255;

    const string ERROR_MESSAGE = "واحد مشخصه اشتباه است";

    public readonly static SpecificationUnitType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private SpecificationUnitType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<SpecificationUnitType> Create(string value) => CreateUnsafe(value).Validate();

    public static SpecificationUnitType CreateUnsafe(string? value) => new(value ?? string.Empty);

    public PrimitiveResult<SpecificationUnitType> Validate() =>
        this.Value.Length > MAX_LENGTH
            ? DomainTypesHelper.CreateTypeErrorResult<SpecificationUnitType>(ERROR_MESSAGE)
            : this;

    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || this.Equals(Empty) || this.Value.Equals(string.Empty);

    public string GetDbValue() => this.Value;

    public static SpecificationUnitType FromDb(string? value)
    {
        var result = Create(value ?? string.Empty);
        return result.IsFailure
            ? Empty
            : result.Value;
    }
}
