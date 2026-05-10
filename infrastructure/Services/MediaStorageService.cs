using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace infrastructure.Services;

public class MediaStorageService : IMediaStorageService
{
    private readonly string _basePath;
    private readonly string _baseUrl;

    public MediaStorageService(IConfiguration configuration)
    {
        _basePath = configuration["MediaStorage:BasePath"]!;
        _baseUrl = configuration["MediaStorage:BaseUrl"]!;
    }

    public async Task<string> SaveAsync(IFormFile file, string folder, CancellationToken cancellationToken = default)
    {
        var folderPath = Path.Combine(_basePath, folder);
        Directory.CreateDirectory(folderPath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(folderPath, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        return $"{_baseUrl}/{folder}/{fileName}";
    }

    public async Task DeleteAsync(string url, CancellationToken cancellationToken = default)
    {
        var relativePath = url.Replace(_baseUrl, string.Empty).TrimStart('/');
        var filePath = Path.Combine(_basePath, relativePath);

        if (File.Exists(filePath))
            File.Delete(filePath);

        await Task.CompletedTask;
    }
}
