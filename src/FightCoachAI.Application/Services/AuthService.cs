using FightCoachAI.Application.DTOs.Auth;
using FightCoachAI.Application.Interfaces;
using FightCoachAI.Domain.Entities;
using FightCoachAI.Domain.Exceptions;
using FightCoachAI.Domain.Interfaces;

namespace FightCoachAI.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(
        IUnitOfWork unitOfWork,
        IJwtTokenService jwtTokenService,
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        if (await _unitOfWork.Users.ExistsByEmailAsync(request.Email, cancellationToken))
            throw new DomainException("A user with this email already exists");

        var user = new User
        {
            Email = request.Email.ToLowerInvariant(),
            PasswordHash = _passwordHasher.Hash(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Discipline = request.Discipline,
            ExperienceLevel = request.ExperienceLevel,
        };

        await _unitOfWork.Users.AddAsync(user, cancellationToken);

        var subscription = new Subscription
        {
            UserId = user.Id,
            PlanType = Domain.Enums.SubscriptionPlan.Free,
            Status = Domain.Enums.SubscriptionStatus.Active,
            MonthlyAnalysisLimit = 3,
            MaxVideoDuration = 300,
            Price = 0,
        };
        await _unitOfWork.Subscriptions.AddAsync(subscription, cancellationToken);

        var settings = new UserSettings { UserId = user.Id };
        user.Settings = settings;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToAuthResponse(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(request.Email, cancellationToken)
            ?? throw new DomainException("Invalid email or password");

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new DomainException("Invalid email or password");

        return MapToAuthResponse(user);
    }

    public Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
    {
        var principal = _jwtTokenService.ValidateToken(request.AccessToken)
            ?? throw new DomainException("Invalid access token");

        throw new NotImplementedException("Refresh token rotation will be implemented in a future step");
    }

    private AuthResponse MapToAuthResponse(User user)
    {
        return new AuthResponse
        {
            AccessToken = _jwtTokenService.GenerateAccessToken(user.Id, user.Email, "User"),
            RefreshToken = _jwtTokenService.GenerateRefreshToken(),
            ExpiresAt = DateTime.UtcNow.AddMinutes(15),
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Discipline = user.Discipline,
                ExperienceLevel = user.ExperienceLevel,
            },
        };
    }
}
