using FightCoachAI.Application.Interfaces;
using FightCoachAI.Domain.Interfaces;
using FightCoachAI.Domain.Interfaces.Repositories;
using FightCoachAI.Infrastructure.ExternalServices.Storage;
using FightCoachAI.Infrastructure.Identity;
using FightCoachAI.Infrastructure.Messaging;
using FightCoachAI.Infrastructure.Persistence;
using FightCoachAI.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FightCoachAI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVideoRepository, VideoRepository>();
        services.AddScoped<IAnalysisResultRepository, AnalysisResultRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IAchievementRepository, AchievementRepository>();
        services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
        services.AddScoped<IUserAchievementRepository, UserAchievementRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IVideoStorageService, LocalVideoStorageService>();

        services.AddSingleton<VideoProcessingChannel>();
        services.AddHostedService<VideoProcessingConsumer>();

        return services;
    }
}
