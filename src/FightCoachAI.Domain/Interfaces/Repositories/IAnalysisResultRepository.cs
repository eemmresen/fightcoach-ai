using FightCoachAI.Domain.Entities;

namespace FightCoachAI.Domain.Interfaces.Repositories;

public interface IAnalysisResultRepository : IGenericRepository<AnalysisResult>
{
    Task<AnalysisResult?> GetByVideoIdAsync(Guid videoId, CancellationToken cancellationToken = default);
    Task<IEnumerable<AnalysisResult>> GetHistoryByUserIdAsync(Guid userId, int limit = 20, CancellationToken cancellationToken = default);
}
