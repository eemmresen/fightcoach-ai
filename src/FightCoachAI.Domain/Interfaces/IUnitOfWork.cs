using FightCoachAI.Domain.Interfaces.Repositories;

namespace FightCoachAI.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IVideoRepository Videos { get; }
    IAnalysisResultRepository AnalysisResults { get; }
    ISubscriptionRepository Subscriptions { get; }
    IAchievementRepository Achievements { get; }
    IActivityLogRepository ActivityLogs { get; }
    IUserAchievementRepository UserAchievements { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
