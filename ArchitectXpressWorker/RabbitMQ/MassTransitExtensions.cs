using ArchitectXpressWorker.EventHandlers;
using MassTransit;
using Microsoft.Extensions.Options;

namespace ArchitectXpressWorker.RabbitMQ;

public static class MassTransitExtensions
{
    public static void MassTransitConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMQSettings>(configuration.GetSection(nameof(RabbitMQSettings)));

        services.AddMassTransit(config =>
        {
            config.AddConsumer<PassengerAddedEventHandler>();

            config.UsingRabbitMq((ctx, cfg) =>
            {
                var rabbitMqSettings = ctx.GetRequiredService<IOptions<RabbitMQSettings>>().Value;

                cfg.Host(rabbitMqSettings.Host);

                cfg.ReceiveEndpoint(rabbitMqSettings.QueueName, e =>
                {
                    e.Bind(rabbitMqSettings.ExchangeName);
                    e.ConfigureConsumer<PassengerAddedEventHandler>(ctx);
                });
            });
        });
    }
}
