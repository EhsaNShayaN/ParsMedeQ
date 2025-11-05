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
    var path = Path.Combine(projectDir, "Models", "x.cs");
    var modelContent = File.ReadAllText(path);
    var classNameMatch = Regex.Match(modelContent, @"class\s+(\w+)");
    if (!classNameMatch.Success)
    {
        Console.WriteLine("No class name found in file.");
        return;
    }

    var className = classNameMatch.Groups[1].Value;
    var props = Regex.Matches(modelContent, @"public\s+([\w<>?]+)\s+(\w+)\s*{\s*get;\s*set;\s*}");

    // prepare output directory
    var outputDir = Path.Combine(Path.GetDirectoryName(path)!, "Application", "Features", $"{className}Feature");
    Directory.CreateDirectory(outputDir);

    // filenames
    var commandFile = Path.Combine(outputDir, $"Create{className}Command.cs");
    var handlerFile = Path.Combine(outputDir, $"Create{className}CommandHandler.cs");
    var responseFile = Path.Combine(outputDir, $"Create{className}Response.cs");

    // Generate Command
    var commandProps = string.Join(", ", props.Select(p => $"{p.Groups[1].Value} {p.Groups[2].Value}"));
    var commandCode = $@"using SRH.MediatRMessaging;

namespace ParsMedeQ.Application.Features.{className}Features.Create{className}Feature;

public sealed record class Create{className}Command({commandProps}) : IPrimitiveResultCommand<Create{className}Response>, IValidatableRequest<Create{className}>
{{
    public ValueTask<PrimitiveResult<Create{className}Command>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.Title)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure(""Validation.Error"", ""موبایل ارسالی نامعتبر است""))
                ]);
}}
";
    File.WriteAllText(commandFile, commandCode);

    // Generate Response
    var responseCode = $@"public class Create{className}Response
{{
    public int Id {{ get; set; }}
    public string Message {{ get; set; }} = string.Empty;
}}
";
    File.WriteAllText(responseFile, responseCode);

    // Generate Handler
    var handlerCode = $@"using MediatR;

public class Create{className}CommandHandler : IRequestHandler<Create{className}Command, Create{className}Response>
{{
    private readonly I{className}Repository _repository;

    public Create{className}CommandHandler(I{className}Repository repository)
    {{
        _repository = repository;
    }}

    public async Task<Create{className}Response> Handle(Create{className}Command request, CancellationToken cancellationToken)
    {{
        var entity = new {className}
        {{
{string.Join(Environment.NewLine, props.Select(p => $"            {p.Groups[2].Value} = request.{p.Groups[2].Value},"))}
        }};

        await _repository.AddAsync(entity, cancellationToken);

        return new Create{className}Response
        {{
            Id = entity.Id,
            Message = ""{className} created successfully""
        }};
    }}
}}
";
    File.WriteAllText(handlerFile, handlerCode);

    Console.WriteLine($"✅ Generated files in: {outputDir}");
}