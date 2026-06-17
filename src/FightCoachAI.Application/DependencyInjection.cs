using FightCoachAI.Application.Interfaces;
using FightCoachAI.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FightCoachAI.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserSettingsService, UserSettingsService>();
        services.AddScoped<IVideoService, VideoService>();
        services.AddScoped<IAnalysisService, AnalysisService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<IAchievementService, AchievementService>();
        return services;
    }
}
