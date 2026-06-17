using FightCoachAI.Domain.Entities;

namespace FightCoachAI.Domain.Interfaces.Repositories;

public interface IUserAchievementRepository : IGenericRepository<UserAchievement>
{
    Task<IEnumerable<UserAchievement>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
