using Scriban;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ValueConvertersGenerator;
internal static class CRUDGenerator
{
    static string BaseDir = AppContext.BaseDirectory;
    static string ProjectDir = Path.GetFullPath(Path.Combine(BaseDir, @"..\..\.."));
    static string ModelPath = Path.Combine(ProjectDir, "Models", "Service.cs");
    static string ModelTranslationPath = Path.Combine(ProjectDir, "Models", "ServiceTranslation.cs");
    static bool HasTranslation = true;
    internal static void GenerateCRUD()
    {
        var modelContent0 = File.ReadAllText(ModelPath);
        var classNameMatch = Regex.Match(modelContent0, @"class\s+(\w+)");

        string modelTranslationContent = string.Empty;
        Match translationClassNameMatch = null;
        if (HasTranslation)
        {
            modelTranslationContent = File.ReadAllText(ModelTranslationPath);
            translationClassNameMatch = Regex.Match(modelTranslationContent, @"class\s+(\w+)");
        }
        if (!classNameMatch.Success)
        {
            Console.WriteLine("No class name found in file.");
            return;
        }

        var className = classNameMatch.Groups[1].Value;
        var props = Regex.Matches(modelContent0, @"public\s+([\w<>?]+)\s+(\w+)\s*{\s*get;\s*private set;\s*}");
        var properties = props.Select(p => new Prop
        {
            DataType = p.Groups[1].Value,
            Name = p.Groups[2].Value,
        }).ToList();
        if (HasTranslation)
        {
            var translationProps = Regex.Matches(modelTranslationContent, @"public\s+([\w<>?]+)\s+(\w+)\s*{\s*get;\s*private set;\s*}");
            properties.AddRange(translationProps.Select(p => new Prop
            {
                DataType = p.Groups[1].Value,
                Name = p.Groups[2].Value,
            }).ToList());
        }
        properties.RemoveAll(s => s.Name == className);
        foreach (var prop in properties)
        {
            if (prop.Name == "Image" || prop.Name == "File")
            {
                prop.Name += "Info";
                prop.DataType = "FileData?";
            }
        }

        // filenames

        var model = new TemplateModel
        {
            ClassName = className,
            CamelClassName = ToCamelCaseExtended(className),
            FirstPropName = props[0].Groups[2].Value,
            Props = properties
        };

        List<string> lst = new List<string> { "Create", "Update" };
        foreach (var item in lst)
        {
            // prepare output directory
            var outputDir = Path.Combine(ProjectDir, "Application", "Features", $"{className}Features", $"{item}{className}Feature");
            Directory.CreateDirectory(outputDir);

            var templateCommandPath = Path.Combine(ProjectDir, "Application", "Features", "TemplateFeatures", $"{item}TemplateFeature", $"{item}TemplateCommand.txt");
            var commandFile = Path.Combine(outputDir, $"{item}{className}Command.cs");
            GenerateCommand(templateCommandPath, commandFile, model);

            var templateHandlerPath = Path.Combine(ProjectDir, "Application", "Features", "TemplateFeatures", $"{item}TemplateFeature", $"{item}TemplateCommandHandler.txt");
            var handlerFile = Path.Combine(outputDir, $"{item}{className}CommandHandler.cs");
            GenerateCommand(templateHandlerPath, handlerFile, model);

            var templateResponsePath = Path.Combine(ProjectDir, "Application", "Features", "TemplateFeatures", $"{item}TemplateFeature", $"{item}TemplateCommandResponse.txt");
            var responseFile = Path.Combine(outputDir, $"{item}{className}CommandResponse.cs");
            GenerateCommand(templateResponsePath, responseFile, model);
        }

        Console.WriteLine("✅ Generated files");
    }

    static void GenerateCommand(string templatePath, string outputPath, TemplateModel model)
    {
        // Load the template file
        string templateText = File.ReadAllText(templatePath);

        // Compile the template
        var template = Template.Parse(templateText);

        // Render the final text
        string result = template.Render(model, member => member.Name);

        // Save the rendered result
        File.WriteAllText(outputPath, result);

        Console.WriteLine($"✅ File generated successfully: {Path.GetFullPath(outputPath)}");
    }
    public static string ToCamelCaseExtended(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        // Replace separators with spaces
        string cleaned = Regex.Replace(input, @"[-_]", " ");

        // Title case words
        TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
        string pascal = textInfo.ToTitleCase(cleaned).Replace(" ", "");

        // Convert to camelCase
        return char.ToLowerInvariant(pascal[0]) + pascal.Substring(1);
    }
}
public sealed class TemplateModel
{
    public string ClassName { get; set; }
    public string CamelClassName { get; set; }
    public string FirstPropName { get; set; }
    public List<Prop> Props { get; set; }
}
public sealed class Prop
{
    public string DataType { get; set; }
    public string Name { get; set; }
}