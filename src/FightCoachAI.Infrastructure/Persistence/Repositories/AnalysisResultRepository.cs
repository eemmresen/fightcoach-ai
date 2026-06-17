using FightCoachAI.Domain.Entities;
using FightCoachAI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FightCoachAI.Infrastructure.Persistence.Repositories;

public class AnalysisResultRepository : GenericRepository<AnalysisResult>, IAnalysisResultRepository
{
    public AnalysisResultRepository(AppDbContext context) : base(context) { }

    public async Task<AnalysisResult?> GetByVideoIdAsync(Guid videoId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(a => a.VideoId == videoId, cancellationToken);
    }

    public async Task<IEnumerable<AnalysisResult>> GetHistoryByUserIdAsync(Guid userId, int limit = 20, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(a => a.Video)
            .Where(a => a.Video!.UserId == userId)
            .OrderByDescending(a => a.CreatedAt)
            .Take(limit)
            .ToListAsync(cancellationToken);
    }
}
