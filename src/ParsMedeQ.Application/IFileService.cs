using Microsoft.AspNetCore.Http;

namespace ParsMedeQ.Application;
public interface IFileService
{
    Task<string> UploadFile(IFormFile file, string folderName, CancellationToken cancellationToken);
    Task<string> UploadFile(byte[] file, string extension, string folderName, CancellationToken cancellationToken);
    Task<(string Path, string MimeType)> FullUploadFile(IFormFile file, string folderName, CancellationToken cancellationToken);
    Task<(string Path, string MimeType)> FullUploadFile(IFormFile file, string folderName, string fileName, CancellationToken cancellationToken);
    Task<(string Path, string MimeType)> FullUploadFile(byte[] file, string folderName, string fileName, CancellationToken cancellationToken);
    string GetMimeType(string fileName);
    string CreateDirectory(string dir, string fileName, out string relativePath);
    void BytesToFile(byte[] bytes, string path);
    byte[] FileToBytes(string path);
    Task<FileData?> ReadStream(IFormFile? file);
}

public readonly record struct FileData(
    byte[] Bytes,
    string Name,
    string MimeType,
    string Extension);