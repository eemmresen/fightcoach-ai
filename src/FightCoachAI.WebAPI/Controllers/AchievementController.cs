using FightCoachAI.Application.DTOs.Achievement;
using FightCoachAI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FightCoachAI.WebAPI.Controllers;

[ApiController]
[Route("api/user")]
[Authorize]
public class AchievementController : ControllerBase
{
    private readonly IAchievementService _achievementService;

    public AchievementController(IAchievementService achievementService)
    {
        _achievementService = achievementService;
    }

    [HttpGet("achievements")]
    public async Task<ActionResult<UserAchievementResponse>> GetAchievements()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        return Ok(await _achievementService.GetUserAchievementsAsync(userId));
    }
}
