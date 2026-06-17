using FightCoachAI.Application.DTOs.Analysis;

namespace FightCoachAI.Application.Interfaces;

public interface IAnalysisService
{
    Task<AnalysisResultResponse> GetByVideoIdAsync(Guid videoId, CancellationToken cancellationToken = default);
    Task<AnalysisHistoryResponse> GetHistoryAsync(Guid userId, int limit = 20, CancellationToken cancellationToken = default);
    Task SaveAnalysisResultAsync(Guid videoId, SaveAnalysisRequest request, CancellationToken cancellationToken = default);
}
