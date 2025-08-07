// See https://aka.ms/new-console-template for more information

using ValueConvertersGenerator;

var dir = new DirectoryInfo(Environment.CurrentDirectory);

var src = FindFolder(dir, 0, "src");
if (src is null)
{
    Console.WriteLine("can not find src folder");
}
var path = Path.Combine(src!.FullName, "EShop.Infrastructure", "Persistance", "ValueConverters", "_auto-generated");
Directory.CreateDirectory(path);
Generator.Generate(path);

static DirectoryInfo? FindFolder(DirectoryInfo? current, int level, string folder)
{
    level++;
    if (current is null) return null;
    var subDirs = current.GetDirectories();
    if (subDirs?.Length == 0) return FindFolder(current.Parent, level, folder);
    var found = subDirs!.FirstOrDefault(x => x.Name.Equals(folder, StringComparison.InvariantCultureIgnoreCase));
    return found ?? FindFolder(current.Parent, level, folder);
}




