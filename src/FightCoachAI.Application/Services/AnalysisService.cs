using FightCoachAI.Application.DTOs.Analysis;
using FightCoachAI.Application.Interfaces;
using FightCoachAI.Domain.Entities;
using FightCoachAI.Domain.Exceptions;
using FightCoachAI.Domain.Interfaces;

namespace FightCoachAI.Application.Services;

public class AnalysisService : IAnalysisService
{
    private readonly IUnitOfWork _unitOfWork;

    public AnalysisService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AnalysisResultResponse> GetByVideoIdAsync(Guid videoId, CancellationToken cancellationToken = default)
    {
        var analysis = await _unitOfWork.AnalysisResults.GetByVideoIdAsync(videoId, cancellationToken)
            ?? throw new DomainException("Analysis not found");

        return MapToResponse(analysis);
    }

    public async Task<AnalysisHistoryResponse> GetHistoryAsync(Guid userId, int limit = 20, CancellationToken cancellationToken = default)
    {
        var results = await _unitOfWork.AnalysisResults.GetHistoryByUserIdAsync(userId, limit, cancellationToken);

        return new AnalysisHistoryResponse
        {
            Items = results.Select(MapToResponse).ToList(),
            TotalCount = results.Count(),
        };
    }

    public async Task SaveAnalysisResultAsync(Guid videoId, SaveAnalysisRequest request, CancellationToken cancellationToken = default)
    {
        var existing = await _unitOfWork.AnalysisResults.GetByVideoIdAsync(videoId, cancellationToken);

        if (existing is not null)
        {
            existing.FightIq = request.FightIq;
            existing.GuardScore = request.GuardScore;
            existing.DefenseScore = request.DefenseScore;
            existing.FootworkScore = request.FootworkScore;
            existing.AttackScore = request.AttackScore;
            existing.ConsistencyScore = request.ConsistencyScore;
            existing.ErrorsJson = request.ErrorsJson;
            existing.CombinationsJson = request.CombinationsJson;
            existing.AiCoachFeedback = request.AiCoachFeedback;
            existing.ProcessingTimeMs = request.ProcessingTimeMs;
        }
        else
        {
            await _unitOfWork.AnalysisResults.AddAsync(new AnalysisResult
            {
                VideoId = videoId,
                FightIq = request.FightIq,
                GuardScore = request.GuardScore,
                DefenseScore = request.DefenseScore,
                FootworkScore = request.FootworkScore,
                AttackScore = request.AttackScore,
                ConsistencyScore = request.ConsistencyScore,
                ErrorsJson = request.ErrorsJson,
                CombinationsJson = request.CombinationsJson,
                AiCoachFeedback = request.AiCoachFeedback,
                ProcessingTimeMs = request.ProcessingTimeMs,
            }, cancellationToken);
        }

        var video = await _unitOfWork.Videos.GetByIdAsync(videoId, cancellationToken);
        if (video is not null)
        {
            video.Status = Domain.Enums.VideoStatus.Completed;
            video.CompletedAt = DateTime.UtcNow;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static AnalysisResultResponse MapToResponse(AnalysisResult a) => new()
    {
        Id = a.Id,
        VideoId = a.VideoId,
        FightIq = a.FightIq,
        GuardScore = a.GuardScore,
        DefenseScore = a.DefenseScore,
        FootworkScore = a.FootworkScore,
        AttackScore = a.AttackScore,
        ConsistencyScore = a.ConsistencyScore,
        AiCoachFeedback = a.AiCoachFeedback,
        ProcessingTimeMs = a.ProcessingTimeMs,
        CreatedAt = a.CreatedAt,
    };
}
