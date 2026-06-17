using FightCoachAI.Application.DTOs.User;

namespace FightCoachAI.Application.Interfaces;

public interface IUserSettingsService
{
    Task<UserSettingsResponse> GetSettingsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<UserSettingsResponse> UpdateSettingsAsync(Guid userId, UpdateSettingsRequest request, CancellationToken cancellationToken = default);
}
