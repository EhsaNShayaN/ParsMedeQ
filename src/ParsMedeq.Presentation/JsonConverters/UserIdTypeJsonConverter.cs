using EShop.Domain.Helpers;
using EShop.Domain.Types.UserId;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EShop.Presentation.JsonConverters;

internal sealed class UserIdTypeJsonConverter : JsonConverter<UserIdType>
{
    public override UserIdType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        UserIdType.FromDb(HashIdsHelper.Instance.DecodeSingle(reader.GetString()));

    public override void Write(Utf8JsonWriter writer, UserIdType value, JsonSerializerOptions options) =>
        writer.WriteStringValue(HashIdsHelper.Instance.Encode(value.Value));
}