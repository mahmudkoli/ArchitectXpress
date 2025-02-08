using ArchitectXpress.Common.MessageBrokerSettings;
using ArchitectXpress.Common.Publisher;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ArchitectXpress.RabbitMQ;

public class MessagePublisher : IMessagePublisher
{
    private readonly IBus _bus;
    private readonly RabbitMQSettings _rabbitMqSettings;
    private readonly ILogger<MessagePublisher> _logger;

    public MessagePublisher(
        IBus bus,
        IOptions<RabbitMQSettings> rabbitMqSettings,
        ILogger<MessagePublisher> logger)
    {
        _bus = bus;
        _rabbitMqSettings = rabbitMqSettings.Value;
        _logger = logger;
    }

    public async Task PublishMessageAsync<T>(T data) where T : class
    {
        _ = data ?? throw new ArgumentException("Data not found for publishing the message.");

        try
        {
            await _bus.Publish(data);
            _logger.LogInformation("Successfully sent message.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to sent message.");
            throw;
        }
    }
}
