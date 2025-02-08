using ArchitectXpress.Common.Publisher;
using ArchitectXpress.RabbitMQ;
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
            config.AddConsumer<CalculateFareEventHandler>();

            config.UsingRabbitMq((ctx, cfg) =>
            {
                var rabbitMqSettings = ctx.GetRequiredService<IOptions<RabbitMQSettings>>().Value;

                cfg.Host(rabbitMqSettings.Host);

                cfg.UseRawJsonSerializer(RawSerializerOptions.All, isDefault: true);
                cfg.UseRawJsonDeserializer(RawSerializerOptions.All, isDefault: true);
                cfg.Message<CalculateFareEventHandler>(e => e.SetEntityName(rabbitMqSettings.FareExchangeName));

                cfg.ReceiveEndpoint(rabbitMqSettings.QueueName, e =>
                {
                    e.ConfigureConsumeTopology = false;
                    e.Bind(rabbitMqSettings.ExchangeName);
                    e.ConfigureConsumer<PassengerAddedEventHandler>(ctx);
                    e.ConfigureConsumer<CalculateFareEventHandler>(ctx);
                });
                cfg.ReceiveEndpoint(rabbitMqSettings.FareQueueName, e =>
                {
                    e.ConfigureConsumeTopology = false;
                    e.Bind(rabbitMqSettings.FareExchangeName);
                    e.ConfigureConsumer<CalculateFareEventHandler>(ctx);
                });
            });
            services.AddSingleton<IMessagePublisher, MessagePublisher>();
        });
    }
}
