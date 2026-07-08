using FCG.Notifications.Application.Messaging;
using FCG.Notifications.Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCG.Notifications.Infrastructure;

public static class DependencyInjectionInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMessageBus, MessageBus>();
        services.AddRabbitMqTopologyInitializer(configuration);

        return services;
    }

    public static IServiceCollection AddHealthChecksInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitHost = configuration["MessageBusConfigs:Host"]
            ?? throw new InvalidOperationException("MessageBusConfigs:Host não configurado.");

        services.AddHealthChecks()
            .AddRabbitMQ(rabbitHost, name: "rabbitmq");

        return services;
    }
}
