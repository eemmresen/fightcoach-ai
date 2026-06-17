using FightCoachAI.Domain.Entities;
using FightCoachAI.Domain.Interfaces;
using System.Security.Claims;

namespace FightCoachAI.WebAPI.Middleware;

public class ActivityLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public ActivityLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
    {
        await _next(context);

        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim is null) return;

        var activityLog = new ActivityLog
        {
            UserId = Guid.Parse(userIdClaim),
            Action = $"{context.Request.Method} {context.Request.Path}",
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            UserAgent = context.Request.Headers.UserAgent.ToString(),
            CreatedAt = DateTime.UtcNow,
        };

        await unitOfWork.ActivityLogs.AddAsync(activityLog);
        await unitOfWork.SaveChangesAsync();
    }
}
