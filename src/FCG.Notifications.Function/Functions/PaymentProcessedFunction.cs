using System.Text.Json;
using FCG.Notifications.Function.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FCG.Notifications.Function.Functions;

public class PaymentProcessedFunction
{
    private readonly ILogger<PaymentProcessedFunction> _logger;

    public PaymentProcessedFunction(ILogger<PaymentProcessedFunction> logger) => _logger = logger;

    [Function(nameof(PaymentProcessedFunction))]
    public void Run(
        [RabbitMQTrigger("notifications.payment-processed-queue", ConnectionStringSetting = "RabbitMQ")] string message)
    {
        var dados = JsonSerializer.Deserialize<PaymentProcessedEvent>(message);
        if (dados is null)
        {
            _logger.LogWarning("[EMAIL] Mensagem PaymentProcessed inválida: {Message}", message);
            return;
        }

        if (!string.Equals(dados.Status, "Approved", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogInformation(
                "[EMAIL] Pagamento não aprovado — e-mail de confirmação não enviado | OrderId: {OrderId}",
                dados.OrderId);
            return;
        }

        _logger.LogInformation(
            "[EMAIL] Confirmação de compra enviada | UserId: {UserId} | GameId: {GameId} | OrderId: {OrderId}",
            dados.UserId,
            dados.GameId,
            dados.OrderId);
    }
}
