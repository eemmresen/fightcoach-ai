using FightCoachAI.Domain.Entities;

namespace FightCoachAI.Domain.Interfaces.Repositories;

public interface IVideoRepository : IGenericRepository<Video>
{
    Task<IEnumerable<Video>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Video?> GetWithAnalysisAsync(Guid videoId, CancellationToken cancellationToken = default);
}
