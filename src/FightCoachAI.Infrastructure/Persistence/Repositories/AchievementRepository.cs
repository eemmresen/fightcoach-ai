using FightCoachAI.Domain.Entities;
using FightCoachAI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FightCoachAI.Infrastructure.Persistence.Repositories;

public class AchievementRepository : GenericRepository<Achievement>, IAchievementRepository
{
    public AchievementRepository(AppDbContext context) : base(context) { }

    public new async Task<IEnumerable<Achievement>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.OrderBy(a => a.Name).ToListAsync(cancellationToken);
    }
}
