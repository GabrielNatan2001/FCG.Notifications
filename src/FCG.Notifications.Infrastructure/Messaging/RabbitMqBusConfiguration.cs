using FCG.Notifications.Application.Messaging.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FCG.Notifications.Infrastructure.Messaging;

internal static class RabbitMqBusConfiguration
{
    public static void ConfigureHost(IRabbitMqBusFactoryConfigurator cfg, IConfiguration configuration)
    {
        var rabbit = configuration.GetSection("RabbitMq");
        cfg.Host(
            rabbit["Host"] ?? "localhost",
            ushort.Parse(rabbit["Port"] ?? "5672"),
            rabbit["VirtualHost"] ?? "/",
            h =>
            {
                h.Username(rabbit["Username"] ?? "guest");
                h.Password(rabbit["Password"] ?? "guest");
            });

        cfg.DeployPublishTopology = false;
    }

    public static void ConfigureConsumers(
        IRabbitMqBusFactoryConfigurator cfg,
        IBusRegistrationContext context,
        IConfiguration configuration)
    {
        var queues = configuration.GetSection("RabbitMq:Queues");
        var userCreatedQueue = queues["NotificationsUserCreated"] ?? "notifications.user-created";
        var paymentProcessedQueue = queues["NotificationsPaymentProcessed"] ?? "notifications.payment-processed";

        cfg.ReceiveEndpoint(userCreatedQueue, e =>
        {
            e.UsePreExistingQueue();

            e.Handler<UserCreatedEvent>(async ctx =>
            {
                var logger = ctx.GetServiceOrCreateInstance<ILoggerFactory>().CreateLogger("FCG.Notifications");
                var msg = ctx.Message;
                logger?.LogInformation(
                    "[EMAIL] Boas-vindas enviado para {Email} | Usuário: {Nome} ({UserId})",
                    msg.Email,
                    msg.Nome,
                    msg.UserId);
                await Task.CompletedTask;
            });
        });

        cfg.ReceiveEndpoint(paymentProcessedQueue, e =>
        {
            e.UsePreExistingQueue();

            e.Handler<PaymentProcessedEvent>(async ctx =>
            {
                var logger = ctx.GetServiceOrCreateInstance<ILoggerFactory>().CreateLogger("FCG.Notifications");
                var msg = ctx.Message;

                if (!string.Equals(msg.Status, "Approved", StringComparison.OrdinalIgnoreCase))
                {
                    logger?.LogInformation(
                        "[EMAIL] Pagamento não aprovado — confirmação não enviada | OrderId: {OrderId}",
                        msg.OrderId);
                    return;
                }

                logger?.LogInformation(
                    "[EMAIL] Confirmação de compra enviada | UserId: {UserId} | GameId: {GameId} | OrderId: {OrderId}",
                    msg.UserId,
                    msg.GameId,
                    msg.OrderId);

                await Task.CompletedTask;
            });
        });
    }
}
