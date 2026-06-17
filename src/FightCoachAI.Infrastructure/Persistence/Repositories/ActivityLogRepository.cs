using FightCoachAI.Domain.Entities;
using FightCoachAI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FightCoachAI.Infrastructure.Persistence.Repositories;

public class ActivityLogRepository : GenericRepository<ActivityLog>, IActivityLogRepository
{
    public ActivityLogRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<ActivityLog>> GetByUserIdAsync(Guid userId, int limit = 50, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.CreatedAt)
            .Take(limit)
            .ToListAsync(cancellationToken);
    }
}
