using FightCoachAI.Application.DTOs.Achievement;

namespace FightCoachAI.Application.Interfaces;

public interface IAchievementService
{
    Task<UserAchievementResponse> GetUserAchievementsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task CheckAndAwardAsync(Guid userId, CancellationToken cancellationToken = default);
}
