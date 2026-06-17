using FightCoachAI.Application.DTOs.Achievement;
using FightCoachAI.Application.Interfaces;
using FightCoachAI.Domain.Interfaces;

namespace FightCoachAI.Application.Services;

public class AchievementService : IAchievementService
{
    private readonly IUnitOfWork _unitOfWork;

    public AchievementService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserAchievementResponse> GetUserAchievementsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var achievements = await _unitOfWork.Achievements.GetAllAsync(cancellationToken);
        var userAchievements = await _unitOfWork.UserAchievements.GetByUserIdAsync(userId, cancellationToken);

        var uaDict = userAchievements.ToDictionary(ua => ua.AchievementId);

        var totalXp = 0;
        foreach (var ach in achievements)
        {
            if (uaDict.TryGetValue(ach.Id, out var ua) && ua.IsCompleted)
                totalXp += ach.XpReward;
        }

        return new UserAchievementResponse
        {
            CurrentRank = "Blue Belt Technician",
            TotalXp = totalXp,
            NextRankXp = 15000,
            EarnedBadges = achievements.Where(a => uaDict.TryGetValue(a.Id, out var ua) && ua.IsCompleted).Select(a => new EarnedBadgeDto
            {
                Name = a.Name,
                IconUrl = a.IconUrl ?? "",
                CompletedAt = uaDict[a.Id].CompletedAt ?? DateTime.UtcNow,
            }).ToList(),
            InProgress = achievements.Where(a => !uaDict.ContainsKey(a.Id) || !uaDict[a.Id].IsCompleted).Select(a => new InProgressBadgeDto
            {
                Name = a.Name,
                CurrentValue = uaDict.TryGetValue(a.Id, out var ua) ? ua.CurrentValue : 0,
                TargetValue = a.TargetValue,
            }).ToList(),
        };
    }

    public async Task CheckAndAwardAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var achievements = await _unitOfWork.Achievements.GetAllAsync(cancellationToken);
        var analysisHistory = await _unitOfWork.AnalysisResults.GetHistoryByUserIdAsync(userId, 100, cancellationToken);
        var sessionCount = analysisHistory.Count();

        foreach (var achievement in achievements)
        {
            var userAch = (await _unitOfWork.UserAchievements.GetByUserIdAsync(userId, cancellationToken))
                .FirstOrDefault(ua => ua.AchievementId == achievement.Id);

            if (userAch?.IsCompleted == true) continue;

            if (userAch is null)
            {
                userAch = new Domain.Entities.UserAchievement
                {
                    UserId = userId,
                    AchievementId = achievement.Id,
                    CurrentValue = sessionCount,
                };
                await _unitOfWork.UserAchievements.AddAsync(userAch, cancellationToken);
            }
            else
            {
                userAch.CurrentValue = sessionCount;
            }

            if (userAch.CurrentValue >= achievement.TargetValue)
            {
                userAch.IsCompleted = true;
                userAch.CompletedAt = DateTime.UtcNow;
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
