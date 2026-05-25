using FCG.Notifications.Application.Messaging.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FCG.Notifications.Application.Messaging.Consumers;

public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
{
    private readonly ILogger<UserCreatedConsumer> _logger;

    public UserCreatedConsumer(ILogger<UserCreatedConsumer> logger) => _logger = logger;

    public Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var msg = context.Message;
        _logger.LogInformation(
            "[EMAIL] Boas-vindas enviado para {Email} | Usuário: {Nome} ({UserId})",
            msg.Email,
            msg.Nome,
            msg.UserId);

        return Task.CompletedTask;
    }
}
