using FCG.Notifications.Application.Messaging.Events;

namespace FCG.Notifications.Application.Messaging.Consumers;

public interface IPaymentProcessedMessage
{
    Task Consumir(PaymentProcessedEvent dados);
}
