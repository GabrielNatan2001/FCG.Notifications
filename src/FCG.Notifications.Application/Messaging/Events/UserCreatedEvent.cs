using MassTransit;

namespace FCG.Notifications.Application.Messaging.Events;

[EntityName("fcg.user.created")]
public record UserCreatedEvent(Guid UserId, string Email, string Nome, DateTime CreatedAtUtc);
