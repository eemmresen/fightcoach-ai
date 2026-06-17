using FightCoachAI.Application.DTOs.Subscription;
using FightCoachAI.Application.Interfaces;
using FightCoachAI.Domain.Exceptions;
using FightCoachAI.Domain.Interfaces;

namespace FightCoachAI.Application.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly IUnitOfWork _unitOfWork;

    public SubscriptionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SubscriptionResponse> GetCurrentAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var sub = await _unitOfWork.Subscriptions.GetActiveByUserIdAsync(userId, cancellationToken)
            ?? throw new DomainException("No active subscription found");

        return new SubscriptionResponse
        {
            Id = sub.Id,
            PlanType = sub.PlanType,
            Status = sub.Status,
            StartDate = sub.StartDate,
            EndDate = sub.EndDate,
            MonthlyAnalysisLimit = sub.MonthlyAnalysisLimit,
            MaxVideoDuration = sub.MaxVideoDuration,
            Price = sub.Price,
        };
    }

    public Task<SubscriptionPlansResponse> GetPlansAsync()
    {
        return Task.FromResult(new SubscriptionPlansResponse
        {
            Plans = new List<PlanDto>
            {
                new() { Name = "Free", Price = 0, AnalysisLimit = 3, MaxDuration = 300, Features = new() { "Basic Fight IQ", "3 Analyses/Month", "Basic Error Detection" } },
                new() { Name = "Premium", Price = 19.99m, AnalysisLimit = -1, MaxDuration = 900, Features = new() { "Unlimited Analyses", "AI Coach", "Growth Tracking", "PDF Reports" } },
                new() { Name = "Pro Coach", Price = 49.99m, AnalysisLimit = -1, MaxDuration = 1800, Features = new() { "Everything in Premium", "Multi-Athlete", "Gym Dashboard", "API Access" } },
            },
        });
    }
}
