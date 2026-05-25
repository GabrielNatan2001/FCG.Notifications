using FCG.Notifications.Application.Messaging.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FCG.Notifications.Application.Messaging.Consumers;

public class PaymentProcessedConsumer : IConsumer<PaymentProcessedEvent>
{
    private readonly ILogger<PaymentProcessedConsumer> _logger;

    public PaymentProcessedConsumer(ILogger<PaymentProcessedConsumer> logger) => _logger = logger;

    public Task Consume(ConsumeContext<PaymentProcessedEvent> context)
    {
        var msg = context.Message;

        if (!string.Equals(msg.Status, "Approved", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogInformation(
                "[EMAIL] Pagamento não aprovado — e-mail de confirmação não enviado | OrderId: {OrderId}",
                msg.OrderId);
            return Task.CompletedTask;
        }

        _logger.LogInformation(
            "[EMAIL] Confirmação de compra enviada | UserId: {UserId} | GameId: {GameId} | OrderId: {OrderId}",
            msg.UserId,
            msg.GameId,
            msg.OrderId);

        return Task.CompletedTask;
    }
}
