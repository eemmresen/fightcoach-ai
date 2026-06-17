using System.Security.Claims;

namespace FightCoachAI.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateAccessToken(Guid userId, string email, string role);
    string GenerateRefreshToken();
    ClaimsPrincipal? ValidateToken(string token);
}
