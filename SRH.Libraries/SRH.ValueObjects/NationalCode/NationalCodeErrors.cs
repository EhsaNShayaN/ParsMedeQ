namespace SRH.ValueObjects;

public static partial class ValueObjectErrors
{
    private const string ValueObjectsErrorCode = "ValueObjectsError";
    public readonly static PrimitiveError NationalCodeIsEmptyError = PrimitiveError.Create(ValueObjectsErrorCode, "NationalCode can not be empty");
    public readonly static PrimitiveError NationalCodeIsInvalidError = PrimitiveError.Create(ValueObjectsErrorCode, "NationalCode has invalid format");
}