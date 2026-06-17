using FightCoachAI.Domain.Entities;
using FightCoachAI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FightCoachAI.Infrastructure.Persistence.Repositories;

public class VideoRepository : GenericRepository<Video>, IVideoRepository
{
    public VideoRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Video>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(v => v.UserId == userId)
            .OrderByDescending(v => v.UploadedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Video?> GetWithAnalysisAsync(Guid videoId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(v => v.AnalysisResult)
            .FirstOrDefaultAsync(v => v.Id == videoId, cancellationToken);
    }
}
