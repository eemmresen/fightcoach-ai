using FightCoachAI.Domain.Entities;

namespace FightCoachAI.Domain.Interfaces.Repositories;

public interface IActivityLogRepository : IGenericRepository<ActivityLog>
{
    Task<IEnumerable<ActivityLog>> GetByUserIdAsync(Guid userId, int limit = 50, CancellationToken cancellationToken = default);
}
