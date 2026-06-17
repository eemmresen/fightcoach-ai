using System.Threading.Channels;

namespace FightCoachAI.Infrastructure.Messaging;

public class VideoProcessingChannel
{
    private readonly Channel<VideoProcessingMessage> _channel = Channel.CreateBounded<VideoProcessingMessage>(100);

    public ChannelWriter<VideoProcessingMessage> Writer => _channel.Writer;
    public ChannelReader<VideoProcessingMessage> Reader => _channel.Reader;
}

public class VideoProcessingMessage
{
    public Guid VideoId { get; set; }
    public Guid UserId { get; set; }
    public string VideoUrl { get; set; } = string.Empty;
    public string Discipline { get; set; } = "Boxing";
}
