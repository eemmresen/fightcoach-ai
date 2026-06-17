namespace FightCoachAI.Application.Interfaces;

public interface IVideoStorageService
{
    Task<string> SaveAsync(string fileName, Stream stream, CancellationToken cancellationToken = default);
    Task<Stream> GetAsync(string filePath, CancellationToken cancellationToken = default);
    Task DeleteAsync(string filePath, CancellationToken cancellationToken = default);
    string GetPublicUrl(string filePath);
}
