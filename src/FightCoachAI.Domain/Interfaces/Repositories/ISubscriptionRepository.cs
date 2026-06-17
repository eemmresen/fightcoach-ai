using FightCoachAI.Domain.Entities;

namespace FightCoachAI.Domain.Interfaces.Repositories;

public interface ISubscriptionRepository : IGenericRepository<Subscription>
{
    Task<Subscription?> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
