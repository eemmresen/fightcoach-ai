using FightCoachAI.Application.DTOs.Subscription;

namespace FightCoachAI.Application.Interfaces;

public interface ISubscriptionService
{
    Task<SubscriptionResponse> GetCurrentAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<SubscriptionPlansResponse> GetPlansAsync();
}
