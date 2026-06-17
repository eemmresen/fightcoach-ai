using FightCoachAI.Application.DTOs.Video;
using FightCoachAI.Application.Interfaces;
using FightCoachAI.Domain.Entities;
using FightCoachAI.Domain.Exceptions;
using FightCoachAI.Domain.Interfaces;

namespace FightCoachAI.Application.Services;

public class VideoService : IVideoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVideoStorageService _storageService;

    public VideoService(IUnitOfWork unitOfWork, IVideoStorageService storageService)
    {
        _unitOfWork = unitOfWork;
        _storageService = storageService;
    }

    public async Task<VideoUploadResponse> UploadAsync(Guid userId, string fileName, Stream fileStream, CancellationToken cancellationToken = default)
    {
        var sub = await _unitOfWork.Subscriptions.GetActiveByUserIdAsync(userId, cancellationToken)
            ?? throw new DomainException("No active subscription found");

        var thisMonthVideos = await _unitOfWork.Videos.GetByUserIdAsync(userId, cancellationToken);
        var monthlyUsed = thisMonthVideos.Count(v => v.UploadedAt.Month == DateTime.UtcNow.Month);

        if (sub.MonthlyAnalysisLimit > 0 && monthlyUsed >= sub.MonthlyAnalysisLimit)
            throw new VideoLimitExceededException(monthlyUsed, sub.MonthlyAnalysisLimit);

        var storedPath = await _storageService.SaveAsync(fileName, fileStream, cancellationToken);
        var publicUrl = _storageService.GetPublicUrl(storedPath);

        var video = new Video
        {
            UserId = userId,
            Title = Path.GetFileNameWithoutExtension(fileName),
            OriginalUrl = publicUrl,
            Status = Domain.Enums.VideoStatus.Uploaded,
        };

        await _unitOfWork.Videos.AddAsync(video, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new VideoUploadResponse
        {
            Id = video.Id,
            Status = video.Status,
            Title = video.Title,
            UploadedAt = video.UploadedAt,
        };
    }

    public async Task<VideoListResponse> GetUserVideosAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var videos = await _unitOfWork.Videos.GetByUserIdAsync(userId, cancellationToken);
        var sub = await _unitOfWork.Subscriptions.GetActiveByUserIdAsync(userId, cancellationToken);

        var videoList = new List<VideoDetailResponse>();
        foreach (var v in videos)
        {
            var detail = MapToDetail(v);
            if (v.AnalysisResult is not null)
            {
                detail.Analysis = new AnalysisBriefDto
                {
                    FightIq = v.AnalysisResult.FightIq,
                    GuardScore = v.AnalysisResult.GuardScore,
                    DefenseScore = v.AnalysisResult.DefenseScore,
                    FootworkScore = v.AnalysisResult.FootworkScore,
                    AttackScore = v.AnalysisResult.AttackScore,
                };
            }
            videoList.Add(detail);
        }

        return new VideoListResponse
        {
            Videos = videoList,
            TotalCount = videoList.Count,
            MonthlyUsed = videoList.Count(v => v.UploadedAt.Month == DateTime.UtcNow.Month),
            MonthlyLimit = sub?.MonthlyAnalysisLimit ?? 0,
        };
    }

    public async Task<VideoDetailResponse> GetVideoDetailAsync(Guid videoId, CancellationToken cancellationToken = default)
    {
        var video = await _unitOfWork.Videos.GetWithAnalysisAsync(videoId, cancellationToken)
            ?? throw new DomainException("Video not found");

        var detail = MapToDetail(video);
        if (video.AnalysisResult is not null)
        {
            detail.Analysis = new AnalysisBriefDto
            {
                FightIq = video.AnalysisResult.FightIq,
                GuardScore = video.AnalysisResult.GuardScore,
                DefenseScore = video.AnalysisResult.DefenseScore,
                FootworkScore = video.AnalysisResult.FootworkScore,
                AttackScore = video.AnalysisResult.AttackScore,
            };
        }

        return detail;
    }

    private static VideoDetailResponse MapToDetail(Video v) => new()
    {
        Id = v.Id,
        UserId = v.UserId,
        Title = v.Title,
        Description = v.Description,
        OriginalUrl = v.OriginalUrl,
        ThumbnailUrl = v.ThumbnailUrl,
        Duration = v.Duration,
        Resolution = v.Resolution,
        FileSize = v.FileSize,
        Status = v.Status,
        UploadedAt = v.UploadedAt,
        CompletedAt = v.CompletedAt,
    };
}
