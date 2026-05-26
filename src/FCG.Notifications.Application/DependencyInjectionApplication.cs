using FCG.Notifications.Application.Messaging.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace FCG.Notifications.Application;

public static class DependencyInjectionApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserCreatedMessage, UserCreatedConsumer>();
        services.AddScoped<IPaymentProcessedMessage, PaymentProcessedConsumer>();
        return services;
    }
}
