using System.Text.Json;

namespace SRH.Utilities.Helpers;
public static class JsonHelpers
{
    private readonly static JsonSerializerOptions DefaultJsonSerializerOptions = new JsonSerializerOptions()
    {
        AllowTrailingCommas = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public static string Serialize<T>(T? src, JsonSerializerOptions? opts = null)
    {
        return JsonSerializer.Serialize(src, opts ?? DefaultJsonSerializerOptions);
    }
}
