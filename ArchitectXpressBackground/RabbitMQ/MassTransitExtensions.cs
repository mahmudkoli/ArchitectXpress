using MassTransit;
using Microsoft.Extensions.Options;

namespace ArchitectXpressBackground.RabbitMQ;

public static class MassTransitExtensions
{
    public static void MassTransitConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMQ"));

        services.AddMassTransit(config =>
        {
            config.AddConsumer<COMSUserConsumer>();
            config.AddConsumer<COMSOrganizationConsumer>();
            config.AddConsumer<COMSOrganizationLocationConsumer>();

            config.UsingRabbitMq((ctx, cfg) =>
            {
                var rabbitMqSettings = ctx.GetRequiredService<IOptions<RabbitMqSettings>>().Value;

                cfg.Host(rabbitMqSettings.Host, "/", h =>
                {
                    h.Username(rabbitMqSettings.Username);
                    h.Password(rabbitMqSettings.Password);
                });

                cfg.ReceiveEndpoint($"{rabbitMqSettings.UserQueueName}", e =>
                {
                    e.ConfigureConsumeTopology = false;
                    e.Durable = true;
                    e.AutoDelete = false;
                    e.Bind($"{rabbitMqSettings.UserExchangeName}");
                    e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromMinutes(5)));
                    e.ConfigureConsumer<COMSUserConsumer>(ctx);
                });

                cfg.ReceiveEndpoint($"{rabbitMqSettings.OrganizationQueueName}", e =>
                {
                    e.ConfigureConsumeTopology = false;
                    e.Durable = true;
                    e.AutoDelete = false;
                    e.Bind($"{rabbitMqSettings.OrganizationExchangeName}");
                    e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromMinutes(5)));
                    e.ConfigureConsumer<COMSOrganizationConsumer>(ctx);
                });

                cfg.ReceiveEndpoint($"{rabbitMqSettings.OrganizationLocationQueueName}", e =>
                {
                    e.ConfigureConsumeTopology = false;
                    e.Durable = true;
                    e.AutoDelete = false;
                    e.Bind($"{rabbitMqSettings.OrganizationLocationExchangeName}");
                    e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromMinutes(5)));
                    e.ConfigureConsumer<COMSOrganizationLocationConsumer>(ctx);
                });
            });
        });
    }
}
