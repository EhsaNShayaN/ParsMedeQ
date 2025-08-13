using ParsMedeQ.Domain.Types.FullName;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ParsMedeQ.Presentation.JsonConverters;

internal sealed class FullnameTypeJsonConverter : JsonConverter<FullNameType>
{
    public override FullNameType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        FullNameType.Empty;

    public override void Write(Utf8JsonWriter writer, FullNameType value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.GetValue());
}

