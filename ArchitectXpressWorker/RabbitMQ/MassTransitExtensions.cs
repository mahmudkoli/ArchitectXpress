using ArchitectXpress.Common.MessageBrokerSettings;
using ArchitectXpress.Common.Publisher;
using ArchitectXpress.RabbitMQ;
using ArchitectXpressWorker.EventHandlers;
using ArchitectXpressWorker.Models;
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
            config.AddConsumer<CalculateFareEventHandler>();
            config.AddConsumer<FareCalculatedEventHandler>();

            config.UsingRabbitMq((ctx, cfg) =>
            {
                var rabbitMqSettings = ctx.GetRequiredService<IOptions<RabbitMQSettings>>().Value;

                cfg.Host(rabbitMqSettings.Host);

                cfg.UseRawJsonSerializer(RawSerializerOptions.All, isDefault: true);
                cfg.UseRawJsonDeserializer(RawSerializerOptions.All, isDefault: true);
                cfg.Message<FareCalculatedEvent>(e => e.SetEntityName(rabbitMqSettings.FareCalculatedExchangeName));

                cfg.ReceiveEndpoint(rabbitMqSettings.QueueName, e =>
                {
                    e.ConfigureConsumeTopology = false;
                    e.Bind(rabbitMqSettings.ExchangeName);
                    e.ConfigureConsumer<PassengerAddedEventHandler>(ctx);
                });
                cfg.ReceiveEndpoint(rabbitMqSettings.FareCalculationQueueName, e =>
                {
                    e.ConfigureConsumeTopology = false;
                    e.Bind(rabbitMqSettings.FareCalculationExchangeName);
                    e.ConfigureConsumer<CalculateFareEventHandler>(ctx);
                });
                cfg.ReceiveEndpoint(rabbitMqSettings.FareCalculatedQueueName, e =>
                {
                    e.ConfigureConsumeTopology = false;
                    e.Bind(rabbitMqSettings.FareCalculatedExchangeName);
                    e.ConfigureConsumer<FareCalculatedEventHandler>(ctx);
                });
            });
            services.AddSingleton<IMessagePublisher, MessagePublisher>();
        });
    }
}
