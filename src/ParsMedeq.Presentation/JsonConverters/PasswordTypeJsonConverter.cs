using EShop.Domain.Types.Password;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EShop.Presentation.JsonConverters;

internal sealed class PasswordTypeJsonConverter : JsonConverter<PasswordType>
{
    public override PasswordType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        PasswordType.Empty;
    public override void Write(Utf8JsonWriter writer, PasswordType value, JsonSerializerOptions options) =>
        writer.WriteStringValue("*****");
}

