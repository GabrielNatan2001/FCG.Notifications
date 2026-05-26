namespace FCG.Notifications.Application.Messaging.Events;

public record UserCreatedEvent(Guid UserId, string Email, string Nome, DateTime CreatedAtUtc);
