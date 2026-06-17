using FightCoachAI.Domain.Entities;
using FightCoachAI.Domain.Interfaces;
using FightCoachAI.Infrastructure.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace FightCoachAI.Infrastructure.Messaging;

public class VideoProcessingConsumer : BackgroundService
{
    private readonly VideoProcessingChannel _channel;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<VideoProcessingConsumer> _logger;

    public VideoProcessingConsumer(
        VideoProcessingChannel channel,
        IServiceScopeFactory scopeFactory,
        IHttpClientFactory httpClientFactory,
        ILogger<VideoProcessingConsumer> logger)
    {
        _channel = channel;
        _scopeFactory = scopeFactory;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var message in _channel.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                await ProcessMessage(message, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing video {VideoId}", message.VideoId);
            }
        }
    }

    private async Task ProcessMessage(VideoProcessingMessage message, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var video = await unitOfWork.Videos.GetByIdAsync(message.VideoId, ct);
        if (video is null) return;

        video.Status = Domain.Enums.VideoStatus.Processing;
        await unitOfWork.SaveChangesAsync(ct);

        var httpClient = _httpClientFactory.CreateClient("AiService");
        var aiRequest = new
        {
            video_url = message.VideoUrl,
            video_id = message.VideoId.ToString(),
            user_id = message.UserId.ToString(),
            discipline = message.Discipline,
        };

        var response = await httpClient.PostAsJsonAsync("/api/ai/analyze", aiRequest, ct);
        response.EnsureSuccessStatusCode();

        _logger.LogInformation("AI analysis started for video {VideoId}", message.VideoId);
    }
}
