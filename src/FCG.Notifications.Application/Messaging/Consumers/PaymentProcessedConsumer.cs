using FCG.Notifications.Application.Messaging.Events;
using Microsoft.Extensions.Logging;

namespace FCG.Notifications.Application.Messaging.Consumers;

public class PaymentProcessedConsumer : IPaymentProcessedMessage
{
    private readonly ILogger<PaymentProcessedConsumer> _logger;

    public PaymentProcessedConsumer(ILogger<PaymentProcessedConsumer> logger) => _logger = logger;

    public Task Consumir(PaymentProcessedEvent dados)
    {
        if (!string.Equals(dados.Status, "Approved", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogInformation(
                "[EMAIL] Pagamento não aprovado — e-mail de confirmação não enviado | OrderId: {OrderId}",
                dados.OrderId);
            return Task.CompletedTask;
        }

        _logger.LogInformation(
            "[EMAIL] Confirmação de compra enviada | UserId: {UserId} | GameId: {GameId} | OrderId: {OrderId}",
            dados.UserId,
            dados.GameId,
            dados.OrderId);

        return Task.CompletedTask;
    }
}
