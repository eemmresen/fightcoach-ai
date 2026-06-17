using FightCoachAI.Application.DTOs.Video;

namespace FightCoachAI.Application.Interfaces;

public interface IVideoService
{
    Task<VideoUploadResponse> UploadAsync(Guid userId, string fileName, Stream fileStream, CancellationToken cancellationToken = default);
    Task<VideoListResponse> GetUserVideosAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<VideoDetailResponse> GetVideoDetailAsync(Guid videoId, CancellationToken cancellationToken = default);
}
