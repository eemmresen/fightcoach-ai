using FightCoachAI.Application.DTOs.User;
using FightCoachAI.Application.Interfaces;
using FightCoachAI.Domain.Entities;
using FightCoachAI.Domain.Exceptions;
using FightCoachAI.Domain.Interfaces;

namespace FightCoachAI.Application.Services;

public class UserSettingsService : IUserSettingsService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserSettingsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserSettingsResponse> GetSettingsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken)
            ?? throw new DomainException("User not found");

        if (user.Settings is null)
        {
            user.Settings = new UserSettings { UserId = userId };
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return new UserSettingsResponse
        {
            Language = user.Settings.Language,
            Units = user.Settings.Units,
            AiAnalysisAlerts = user.Settings.AiAnalysisAlerts,
            WeeklyReports = user.Settings.WeeklyReports,
            DarkMode = user.Settings.DarkMode,
        };
    }

    public async Task<UserSettingsResponse> UpdateSettingsAsync(Guid userId, UpdateSettingsRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken)
            ?? throw new DomainException("User not found");

        user.Settings ??= new UserSettings { UserId = userId };

        if (request.Language is not null) user.Settings.Language = request.Language;
        if (request.Units is not null) user.Settings.Units = request.Units;
        if (request.AiAnalysisAlerts.HasValue) user.Settings.AiAnalysisAlerts = request.AiAnalysisAlerts.Value;
        if (request.WeeklyReports.HasValue) user.Settings.WeeklyReports = request.WeeklyReports.Value;
        if (request.DarkMode.HasValue) user.Settings.DarkMode = request.DarkMode.Value;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UserSettingsResponse
        {
            Language = user.Settings.Language,
            Units = user.Settings.Units,
            AiAnalysisAlerts = user.Settings.AiAnalysisAlerts,
            WeeklyReports = user.Settings.WeeklyReports,
            DarkMode = user.Settings.DarkMode,
        };
    }
}
