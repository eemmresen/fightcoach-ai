using FightCoachAI.Application.DTOs.User;
using FightCoachAI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FightCoachAI.WebAPI.Controllers;

[ApiController]
[Route("api/user")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserSettingsService _settingsService;

    public UserController(IUserService userService, IUserSettingsService settingsService)
    {
        _userService = userService;
        _settingsService = settingsService;
    }

    [HttpGet("profile")]
    public async Task<ActionResult<UserProfileResponse>> GetProfile()
    {
        var userId = GetUserId();
        return Ok(await _userService.GetProfileAsync(userId));
    }

    [HttpPut("profile")]
    public async Task<ActionResult<UserProfileResponse>> UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        var userId = GetUserId();
        return Ok(await _userService.UpdateProfileAsync(userId, request));
    }

    [HttpGet("settings")]
    public async Task<ActionResult<UserSettingsResponse>> GetSettings()
    {
        var userId = GetUserId();
        return Ok(await _settingsService.GetSettingsAsync(userId));
    }

    [HttpPut("settings")]
    public async Task<ActionResult<UserSettingsResponse>> UpdateSettings([FromBody] UpdateSettingsRequest request)
    {
        var userId = GetUserId();
        return Ok(await _settingsService.UpdateSettingsAsync(userId, request));
    }

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}
