// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;
using ValueConvertersGenerator;

var dir = new DirectoryInfo(Environment.CurrentDirectory);

var src = FindFolder(dir, 0, "src");
if (src is null)
{
    Console.WriteLine("can not find src folder");
}
var path = Path.Combine(src!.FullName, "ParsMedeQ.Infrastructure", "Persistance", "ValueConverters", "_auto-generated");
Directory.CreateDirectory(path);
Generator.Generate(path);
GenerateCRUD();
static DirectoryInfo? FindFolder(DirectoryInfo? current, int level, string folder)
{
    level++;
    if (current is null) return null;
    var subDirs = current.GetDirectories();
    if (subDirs?.Length == 0) return FindFolder(current.Parent, level, folder);
    var found = subDirs!.FirstOrDefault(x => x.Name.Equals(folder, StringComparison.InvariantCultureIgnoreCase));
    return found ?? FindFolder(current.Parent, level, folder);
}

static void GenerateCRUD()
{
    var dir = AppContext.BaseDirectory;
    var projectDir = Path.GetFullPath(Path.Combine(dir, @"..\..\.."));
    var path = Path.Combine(projectDir, "Models", "Service.cs");
    var modelContent = File.ReadAllText(path);
    var classNameMatch = Regex.Match(modelContent, @"class\s+(\w+)");
    if (!classNameMatch.Success)
    {
        Console.WriteLine("No class name found in file.");
        return;
    }

    var className = classNameMatch.Groups[1].Value;
    var props = Regex.Matches(modelContent, @"public\s+([\w<>?]+)\s+(\w+)\s*{\s*get;\s*private set;\s*}");

    // prepare output directory
    var outputDir = Path.Combine(projectDir, "Application", "Features", $"{className}Features", $"Create{className}Feature");
    Directory.CreateDirectory(outputDir);

    // filenames
    var commandFile = Path.Combine(outputDir, $"Create{className}Command.cs");
    var handlerFile = Path.Combine(outputDir, $"Create{className}CommandHandler.cs");
    var responseFile = Path.Combine(outputDir, $"Create{className}CommandResponse.cs");

    var commandTemplatePath = Path.Combine(projectDir, "Application/Features/TemplateFeatures/CreateTemplateFeature", "CreateTemplateCommand.txt");


    GenerateCommand(className, commandFile, props);
    GenerateHandler(className, handlerFile, props);
    GenerateResponse(className, responseFile, props);

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
