namespace SRH.ValueObjects;

public static partial class ValueObjectErrors
{
    public readonly static PrimitiveError BirthdateValueError = PrimitiveError.Create(ValueObjectsErrorCode, "Invalid Birthdate value");
}