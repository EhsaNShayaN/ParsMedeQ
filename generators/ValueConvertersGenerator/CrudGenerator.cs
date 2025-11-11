using Scriban;
using System.Text.RegularExpressions;

namespace ValueConvertersGenerator;
internal static class CRUDGenerator
{
    static string BaseDir = AppContext.BaseDirectory;
    static string ProjectDir = Path.GetFullPath(Path.Combine(BaseDir, @"..\..\.."));
    static string ModelPath = Path.Combine(ProjectDir, "Models", "Service.cs");

    internal static void GenerateCRUD()
    {
        var modelContent = File.ReadAllText(ModelPath);
        var classNameMatch = Regex.Match(modelContent, @"class\s+(\w+)");
        if (!classNameMatch.Success)
        {
            Console.WriteLine("No class name found in file.");
            return;
        }

        var className = classNameMatch.Groups[1].Value;
        var props = Regex.Matches(modelContent, @"public\s+([\w<>?]+)\s+(\w+)\s*{\s*get;\s*private set;\s*}");

        // prepare output directory
        var outputDir = Path.Combine(ProjectDir, "Application", "Features", $"{className}Features", $"Create{className}Feature");
        Directory.CreateDirectory(outputDir);

        // filenames
        var commandFile = Path.Combine(outputDir, $"Create{className}Command.cs");
        var handlerFile = Path.Combine(outputDir, $"Create{className}CommandHandler.cs");
        var responseFile = Path.Combine(outputDir, $"Create{className}CommandResponse.cs");

        var commandTemplatePath = Path.Combine(ProjectDir, "Application/Features/TemplateFeatures/CreateTemplateFeature", "CreateTemplateCommand.txt");


        var model = new TemplateModel
        {
            ClassName = className,
            FirstPropName = props[0].Groups[2].Value,
            Props = props.Select(p => new Prop
            {
                DataType = p.Groups[1].Value,
                Name = p.Groups[2].Value,
            }).ToList()
        };

        //GenerateCommand(className, commandFile, props);
        GenerateCreateCommand(className, commandFile, model);
        //GenerateHandler(className, handlerFile, props);
        //GenerateResponse(className, responseFile, props);

        Console.WriteLine($"✅ Generated files in: {outputDir}");
    }

    static void GenerateCommand(string className, string commandFile, MatchCollection props)
    {
        // Generate Command
        var commandProps = string.Join(", ", props.Select(p => $"{p.Groups[1].Value} {p.Groups[2].Value}"));
        var commandCode =
    $@"namespace ParsMedeQ.Application.Features.{className}Features.Create{className}Feature;

public sealed record class Create{className}Command({commandProps}) :
    IPrimitiveResultCommand<Create{className}CommandResponse>, IValidatableRequest<Create{className}Command>
{{
    public ValueTask<PrimitiveResult<Create{className}Command>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.{props[0].Groups[2].Value})
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure(""Validation.Error"", ""موبایل ارسالی نامعتبر است""))
                ]);
}}
";
        File.WriteAllText(commandFile, commandCode);
    }

    static void GenerateHandler(string className, string handlerFile, MatchCollection props)
    {
        // Generate Handler
        var handlerProps = string.Join(", ", props.Select(p => $"request.{p.Groups[2].Value}"));
        var handlerCode = $@"using ParsMedeQ.Domain.Aggregates.{className}Aggregate;

namespace ParsMedeQ.Application.Features.{className}Features.Create{className}Feature;

public sealed class Create{className}CommandHandler : IPrimitiveResultCommandHandler<Create{className}Command, Create{className}CommandResponse>
{{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IFileService _fileService;

    public Create{className}CommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IFileService fileService)
    {{
        this._writeUnitOfWork = writeUnitOfWork;
        this._fileService = fileService;
    }}

    public async Task<PrimitiveResult<Create{className}CommandResponse>> Handle(Create{className}Command request, CancellationToken cancellationToken)
    {{
        return await {className}.Create({handlerProps})
            .Map(item => UploadFile(this._fileService, request.ImageInfo.Bytes, request.ImageInfo.Extension, ""Images"", cancellationToken)
                .Map(imagePath => (item, imagePath)))
            .Map(data => data.item.AddTranslation(Constants.LangCode_Farsi.ToLower(), {handlerProps}, data.imagePath)
                .Map(() => data.item))
            .Map({className} => this._writeUnitOfWork.{className}WriteRepository.Create{className}({className})
            .Map({className} => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => {className}))
            .Map({className} => new Create{className}CommandResponse({className} is not null)))
            .ConfigureAwait(false);
    }}

    static async ValueTask<PrimitiveResult<string>> UploadFile(
        IFileService fileService,
        byte[] bytes,
        string fileExtension,
        string fodlerName,
        CancellationToken cancellationToken)
    {{
        if ((bytes?.Length ?? 0) == 0) return string.Empty;
        var result = await fileService.UploadFile(bytes, fileExtension, fodlerName, cancellationToken).ConfigureAwait(false);
        if (string.IsNullOrWhiteSpace(result)) return PrimitiveResult.Failure<string>("""", ""Can not upload file"");

        return result;
    }}
}}
";
        File.WriteAllText(handlerFile, handlerCode);
    }

    static void GenerateResponse(string className, string responseFile, MatchCollection props)
    {
        // Generate Response
        var responseProps = string.Join(", ", props.Select(p => $"{p.Groups[1].Value} {p.Groups[2].Value}"));
        var responseCode =
    $@"namespace ParsMedeQ.Application.Features.{className}Features.Create{className}Feature;

public sealed record Create{className}CommandResponse(
    {responseProps}
);
";
        File.WriteAllText(responseFile, responseCode);
    }

    static void GenerateCreateCommand(string className, string outputPath, TemplateModel model)
    {
        // Load the template file
        var templatePath = Path.Combine(ProjectDir, "Application", "Features", "TemplateFeatures", "CreateTemplateFeature", "CreateTemplateCommand.txt");
        string templateText = File.ReadAllText(templatePath);

        // Compile the template
        var template = Template.Parse(templateText);

        // Render the final text
        string result = template.Render(model, member => member.Name);

        // Save the rendered result
        File.WriteAllText(outputPath, result);

        Console.WriteLine($"✅ File generated successfully: {Path.GetFullPath(outputPath)}");
    }
}
public sealed class TemplateModel
{
    public string ClassName { get; set; }
    public string FirstPropName { get; set; }
    public List<Prop> Props { get; set; }
}
public sealed class Prop
{
    public string DataType { get; set; }
    public string Name { get; set; }
}