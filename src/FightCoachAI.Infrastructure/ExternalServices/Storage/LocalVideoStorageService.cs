using FightCoachAI.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FightCoachAI.Infrastructure.ExternalServices.Storage;

public class LocalVideoStorageService : IVideoStorageService
{
    private readonly string _basePath;

    public LocalVideoStorageService(IConfiguration configuration)
    {
        _basePath = Path.Combine(Directory.GetCurrentDirectory(), configuration["VideoStorage:UploadPath"] ?? "wwwroot/uploads/videos");
        Directory.CreateDirectory(_basePath);
    }

    public async Task<string> SaveAsync(string fileName, Stream stream, CancellationToken cancellationToken = default)
    {
        var uniqueName = $"{Guid.NewGuid()}_{fileName}";
        var filePath = Path.Combine(_basePath, uniqueName);

        using var fileStream = File.Create(filePath);
        await stream.CopyToAsync(fileStream, cancellationToken);

        return uniqueName;
    }

    public Task<Stream> GetAsync(string filePath, CancellationToken cancellationToken = default)
    {
        var fullPath = Path.Combine(_basePath, filePath);
        return Task.FromResult<Stream>(File.OpenRead(fullPath));
    }

    public Task DeleteAsync(string filePath, CancellationToken cancellationToken = default)
    {
        var fullPath = Path.Combine(_basePath, filePath);
        if (File.Exists(fullPath))
            File.Delete(fullPath);
        return Task.CompletedTask;
    }

    public string GetPublicUrl(string filePath) => $"/uploads/videos/{filePath}";
}
