namespace SRH.ValueObjects.PhoneNumber;

public readonly partial record struct SimplePhoneNumberInfo
{
    public readonly static SimplePhoneNumberInfo Empty = new(string.Empty);

    public readonly string Value { get; }

    private SimplePhoneNumberInfo(string value) => Value = value;

    public static SimplePhoneNumberInfo Create(string value) => new(value);

    public bool IsEmpty() => Equals(Empty);
    public static bool IsEmpty(SimplePhoneNumberInfo src) => src.IsEmpty();
}