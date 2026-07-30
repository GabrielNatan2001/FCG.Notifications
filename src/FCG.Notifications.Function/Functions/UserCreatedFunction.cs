using System.Text.Json;
using FCG.Notifications.Function.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FCG.Notifications.Function.Functions;

public class UserCreatedFunction
{
    private readonly ILogger<UserCreatedFunction> _logger;

    public UserCreatedFunction(ILogger<UserCreatedFunction> logger) => _logger = logger;

    [Function(nameof(UserCreatedFunction))]
    public void Run(
        [RabbitMQTrigger("notifications.user-created-queue", ConnectionStringSetting = "RabbitMQ")] string message)
    {
        var dados = JsonSerializer.Deserialize<UserCreatedEvent>(message);
        if (dados is null)
        {
            _logger.LogWarning("[EMAIL] Mensagem UserCreated inválida: {Message}", message);
            return;
        }

        _logger.LogInformation(
            "[EMAIL] Boas-vindas enviado para {Email} | Usuário: {Nome} ({UserId})",
            dados.Email,
            dados.Nome,
            dados.UserId);
    }
}
