using FCG.Notifications.Application.Messaging.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace FCG.Notifications.Application;

public static class DependencyInjectionApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<UserCreatedConsumer>();
        services.AddScoped<PaymentProcessedConsumer>();
        return services;
    }
}
