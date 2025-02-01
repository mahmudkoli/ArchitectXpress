using ArchitectXpress.Models;
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

                cfg.Message<PassengerInfoEvent>(e => e.SetEntityName(rabbitMqSettings.ExchangeName));
            });
        });

        services.AddSingleton<IMessagePublisher, MessagePublisher>();
    }
}
