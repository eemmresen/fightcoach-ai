using FightCoachAI.Application.DTOs.Subscription;
using FightCoachAI.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FightCoachAI.WebAPI.Controllers;

[ApiController]
[Route("api/subscription")]
[Authorize]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet("current")]
    public async Task<ActionResult<SubscriptionResponse>> GetCurrent()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        return Ok(await _subscriptionService.GetCurrentAsync(userId));
    }

    [HttpGet("plans")]
    [AllowAnonymous]
    public async Task<ActionResult<SubscriptionPlansResponse>> GetPlans()
    {
        return Ok(await _subscriptionService.GetPlansAsync());
    }
}
