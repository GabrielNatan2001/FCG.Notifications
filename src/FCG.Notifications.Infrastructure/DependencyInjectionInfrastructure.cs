using FCG.Notifications.Infrastructure.Messaging;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCG.Notifications.Infrastructure;

public static class DependencyInjectionInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                RabbitMqBusConfiguration.ConfigureHost(cfg, configuration);
                RabbitMqBusConfiguration.ConfigureConsumers(cfg, context, configuration);
            });
        });

        return services;
    }
}
