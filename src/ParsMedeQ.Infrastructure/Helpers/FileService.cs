using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using ParsMedeQ.Application;
using ParsMedeQ.Application.Options;

namespace ParsMedeQ.Infrastructure.Helpers;

public class FileService : IFileService
{
    private readonly IOptionsMonitor<RepositoryOptions> _options;

    public FileService(IOptionsMonitor<RepositoryOptions> options)
    {
        this._options = options;
    }
    public async Task<string> UploadFile(IFormFile file, string folderName, CancellationToken cancellationToken)
    {
        var tuple = await this.FullUploadFile(file, folderName, cancellationToken);
        return tuple.Item1;
    }
    public async Task<string> UploadFile(byte[] file, string extension, string folderName, CancellationToken cancellationToken)
    {
        var fileName = Guid.NewGuid() + extension;
        var tuple = await this.FullUploadFile(file, folderName, fileName, cancellationToken);
        return tuple.Item1;
    }
    public async Task<Tuple<string, string>> FullUploadFile(IFormFile file, string folderName, CancellationToken cancellationToken)
    {
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        return await this.FullUploadFile(file, folderName, fileName, cancellationToken);
    }
    public async Task<Tuple<string, string>> FullUploadFile(IFormFile file, string folderName, string fileName, CancellationToken cancellationToken)
    {
        var bytes = await ToByteArrayAsync(file, cancellationToken).ConfigureAwait(false);
        return await this.FullUploadFile(bytes, folderName, fileName, cancellationToken);
        /*var dir = $"{folderName}";
        var path = CreateDirectory(dir, fileName, out string relativePath);
        //////////////////
        await using var stream = File.Create(path);
        await file.CopyToAsync(stream, cancellationToken);
        //////////////////
        var mimetype = GetMimeType(Path.GetFileName(path));
        return new Tuple<string, string>(relativePath, mimetype);*/
    }
    public async Task<Tuple<string, string>> FullUploadFile(byte[] file, string folderName, string fileName, CancellationToken cancellationToken)
    {
        var dir = $"{folderName}";
        var path = CreateDirectory(dir, fileName, out string relativePath);
        //////////////////
        await using var stream = File.Create(path);
        stream.Write(file, 0, file.Length);
        //await file.CopyToAsync(stream, cancellationToken);
        //////////////////
        var mimetype = GetMimeType(Path.GetFileName(path));
        return new Tuple<string, string>(relativePath, mimetype);
    }
    public string GetMimeType(string fileName)
    {
        new FileExtensionContentTypeProvider().TryGetContentType(fileName, out string mimetype);
        return mimetype;
    }
    public string CreateDirectory(string dir, string fileName, out string relativePath)
    {
        var root = _options.CurrentValue.Root;
        relativePath = $"{dir}/{fileName}";
        dir = Path.Combine(root, dir);
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        relativePath = "/Resources/" + relativePath;
        return Path.Combine(dir, fileName);
    }
    public void BytesToFile(byte[] bytes, string path)
    {
        var root = _options.CurrentValue.Root;
        path = Path.Combine(root, path);
        using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write, 4096);
        stream.Write(bytes, 0, bytes.Length);
    }
    public byte[] FileToBytes(string path)
    {
        var root = _options.CurrentValue.Root;
        path = Path.Combine(root, path);
        if (!File.Exists(path)) return null;
        return File.ReadAllBytes(path);
    }
    static async Task<byte[]> ToByteArrayAsync(IFormFile file, CancellationToken cancellationToken)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms, cancellationToken);
        return ms.ToArray();
    }
}