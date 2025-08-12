using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ParsMedeQ.Presentation.Options;

internal sealed class JsonOptionsConfigurator : IConfigureOptions<JsonOptions>
{
    private readonly IServiceProvider _serviceProvider;

    public JsonOptionsConfigurator(IServiceProvider serviceProvider)
    {
        this._serviceProvider = serviceProvider;
    }
    public void Configure(JsonOptions options)
    {
        options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        this.ConfigJsonConverters(options);
    }

    void ConfigJsonConverters(JsonOptions options)
    {
        Assembly[] allAssemblies = [PresentationAssemblyReference.Assembly];

        foreach (var jsonConverterType in allAssemblies.SelectMany(assembly => assembly.DefinedTypes
            .Where(type => type is { IsClass: true, IsAbstract: false } && type.IsAssignableTo(typeof(JsonConverter)))))
        {
            var converter = ActivatorUtilities.CreateInstance(this._serviceProvider, jsonConverterType);
            var jsonConverter = converter as JsonConverter;
            if (jsonConverter is null) continue;
            options.SerializerOptions.Converters.Add(jsonConverter);
        }
    }
}
