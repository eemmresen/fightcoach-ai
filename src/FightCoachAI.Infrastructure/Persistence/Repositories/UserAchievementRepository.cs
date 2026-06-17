using FightCoachAI.Domain.Entities;
using FightCoachAI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FightCoachAI.Infrastructure.Persistence.Repositories;

public class UserAchievementRepository : GenericRepository<UserAchievement>, IUserAchievementRepository
{
    public UserAchievementRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<UserAchievement>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(ua => ua.Achievement)
            .Where(ua => ua.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}
