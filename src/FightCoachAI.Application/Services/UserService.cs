using FightCoachAI.Application.DTOs.User;
using FightCoachAI.Application.Interfaces;
using FightCoachAI.Domain.Exceptions;
using FightCoachAI.Domain.Interfaces;
using FightCoachAI.Domain.Entities;

namespace FightCoachAI.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserProfileResponse> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken)
            ?? throw new DomainException("User not found");

        var videos = await _unitOfWork.Videos.GetByUserIdAsync(userId, cancellationToken);
        var analysisHistory = await _unitOfWork.AnalysisResults.GetHistoryByUserIdAsync(userId, 50, cancellationToken);
        var avgIq = analysisHistory.Any() ? analysisHistory.Average(a => a.FightIq) : (double?)null;
        var maxGuard = analysisHistory.Any() ? analysisHistory.Max(a => a.GuardScore) : (int?)null;

        return new UserProfileResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Discipline = user.Discipline,
            ExperienceLevel = user.ExperienceLevel,
            DateOfBirth = user.DateOfBirth,
            Weight = user.Weight,
            Height = user.Height,
            AvatarUrl = user.AvatarUrl,
            CreatedAt = user.CreatedAt,
            TotalVideos = videos.Count(),
            TotalSessions = analysisHistory.Count(),
            AverageFightIq = avgIq,
            HighestGuardScore = maxGuard.HasValue ? $"{maxGuard}%" : null,
            PeakHandSpeed = null,
        };
    }

    public async Task<UserProfileResponse> UpdateProfileAsync(Guid userId, UpdateProfileRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken)
            ?? throw new DomainException("User not found");

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.AvatarUrl = request.AvatarUrl;
        user.Weight = request.Weight;
        user.Height = request.Height;
        user.DateOfBirth = request.DateOfBirth;

        await _unitOfWork.Users.UpdateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return await GetProfileAsync(userId, cancellationToken);
    }
}
