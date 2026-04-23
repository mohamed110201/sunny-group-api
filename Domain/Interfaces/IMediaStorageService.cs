using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces;

public interface IMediaStorageService
{
    Task<string> SaveAsync(IFormFile file, string folder, CancellationToken cancellationToken = default);
    Task DeleteAsync(string url, CancellationToken cancellationToken = default);
}