using MassTransit;

namespace FCG.Notifications.Infrastructure.Messaging;

internal static class RabbitMqEndpointExtensions
{
    public static void UsePreExistingQueue(this IRabbitMqReceiveEndpointConfigurator endpoint)
    {
        endpoint.ConfigureConsumeTopology = false;
    }
}
