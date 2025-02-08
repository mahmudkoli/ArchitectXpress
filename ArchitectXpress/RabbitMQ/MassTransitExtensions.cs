using Archexpress.Demo.Passenger.Database;
using ArchitectXpress.Common.MessageBrokerSettings;
using ArchitectXpress.Common.Publisher;
using MassTransit;
using Microsoft.Extensions.Options;

namespace ArchitectXpress.RabbitMQ;

public static class MassTransitExtensions
{
    public static void MassTransitConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((ctx, cfg) =>
            {
                var rabbitMqSettings = ctx.GetRequiredService<IOptions<RabbitMQSettings>>().Value;

                cfg.Host(rabbitMqSettings.Host);

                cfg.UseRawJsonSerializer(RawSerializerOptions.All, isDefault: true);

                cfg.Message<Passenger>(e => e.SetEntityName(rabbitMqSettings.ExchangeName));
            });
        });

        services.AddSingleton<IMessagePublisher, MessagePublisher>();
    }
}
