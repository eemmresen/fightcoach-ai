using FightCoachAI.Domain.Entities;
using FightCoachAI.Domain.Interfaces;
using FightCoachAI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace FightCoachAI.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _currentTransaction;

    public UnitOfWork(
        AppDbContext context,
        IUserRepository userRepository,
        IVideoRepository videoRepository,
        IAnalysisResultRepository analysisResultRepository,
        ISubscriptionRepository subscriptionRepository,
        IAchievementRepository achievementRepository,
        IActivityLogRepository activityLogRepository,
        IUserAchievementRepository userAchievementRepository)
    {
        _context = context;
        Users = userRepository;
        Videos = videoRepository;
        AnalysisResults = analysisResultRepository;
        Subscriptions = subscriptionRepository;
        Achievements = achievementRepository;
        ActivityLogs = activityLogRepository;
        UserAchievements = userAchievementRepository;
    }

    public IUserRepository Users { get; }
    public IVideoRepository Videos { get; }
    public IAnalysisResultRepository AnalysisResults { get; }
    public ISubscriptionRepository Subscriptions { get; }
    public IAchievementRepository Achievements { get; }
    public IActivityLogRepository ActivityLogs { get; }
    public IUserAchievementRepository UserAchievements { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction is not null)
        {
            await _currentTransaction.CommitAsync(cancellationToken);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction is not null)
        {
            await _currentTransaction.RollbackAsync(cancellationToken);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public void Dispose()
    {
        _currentTransaction?.Dispose();
        _context.Dispose();
    }
}
