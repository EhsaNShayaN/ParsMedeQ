using SRH.Utilities.Helpers;

namespace ParsMedeq.Domain.Types.VariantTypes;
public readonly partial record struct VariantTypeIdType
{
    public readonly static VariantTypeIdType Unknown = new(0, "نامشخص");
    public readonly static VariantTypeIdType Color = new(1, "رنگ");
    public readonly static VariantTypeIdType Size = new(2, "سایز");
    public readonly static VariantTypeIdType Weight = new(3, "وزن");

    public static IReadOnlyCollection<VariantTypeIdType> AllValues => _allValues
        .Where(x => !x.Value.Equals(Unknown))
        .Select(c => c.Value).ToList().AsReadOnly();

    private readonly static Dictionary<byte, VariantTypeIdType> _allValues = Generate();
    private static Dictionary<byte, VariantTypeIdType> Generate()
    {
        var fieldsOfType = TypeHelpers.GetAllStaticFieldsOfType<VariantTypeIdType>().ToList();
        return fieldsOfType.ToDictionary(x => x.Id, x => x);
    }

    public static VariantTypeIdType FromId(byte id)
    {
        if (_allValues.TryGetValue(id, out var r)) return r;
        return Unknown;
    }
}
