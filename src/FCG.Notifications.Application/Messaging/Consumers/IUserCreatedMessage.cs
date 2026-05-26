using FCG.Notifications.Application.Messaging.Events;

namespace FCG.Notifications.Application.Messaging.Consumers;

public interface IUserCreatedMessage
{
    Task Consumir(UserCreatedEvent dados);
}
