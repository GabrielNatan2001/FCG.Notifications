using FCG.Notifications.Application.Messaging.Events;
using Microsoft.Extensions.Logging;

namespace FCG.Notifications.Application.Messaging.Consumers;

public class UserCreatedConsumer : IUserCreatedMessage
{
    private readonly ILogger<UserCreatedConsumer> _logger;

    public UserCreatedConsumer(ILogger<UserCreatedConsumer> logger) => _logger = logger;

    public Task Consumir(UserCreatedEvent dados)
    {
        _logger.LogInformation(
            "[EMAIL] Boas-vindas enviado para {Email} | Usuário: {Nome} ({UserId})",
            dados.Email,
            dados.Nome,
            dados.UserId);

        return Task.CompletedTask;
    }
}
