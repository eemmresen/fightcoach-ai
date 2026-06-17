using FightCoachAI.Domain.Entities;

namespace FightCoachAI.Domain.Interfaces.Repositories;

public interface IAchievementRepository : IGenericRepository<Achievement>
{
    new Task<IEnumerable<Achievement>> GetAllAsync(CancellationToken cancellationToken = default);
}
