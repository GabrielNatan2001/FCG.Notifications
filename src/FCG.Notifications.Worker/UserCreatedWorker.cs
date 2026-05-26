using FCG.Notifications.Application.Messaging;
using FCG.Notifications.Application.Messaging.Consumers;
using FCG.Notifications.Application.Messaging.Events;
using Microsoft.Extensions.Options;

namespace FCG.Notifications.Worker;

public class UserCreatedWorker : BackgroundService
{
    public readonly IServiceProvider _serviceProvider;
    private readonly ILogger<UserCreatedWorker> _logger;
    private readonly UserCreatedWorkerConfig _config;
    private const string NomeWorker = "WORKER-NOTIFICATIONS-USER-CREATED";

    public UserCreatedWorker(
        IServiceProvider serviceProvider,
        ILogger<UserCreatedWorker> logger,
        IOptions<UserCreatedWorkerConfig> config)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _config = config.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            if (!_config.Ativo)
            {
                _logger.LogInformation("[WORKER][{Nome}] - Esta desativada.", NomeWorker);
                return;
            }

            _logger.LogInformation("[WORKER][{Nome}] Iniciado.", NomeWorker);

            using var scope = _serviceProvider.CreateScope();
            var _messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();
            await _messageBus.Subscribe<UserCreatedEvent>(
                _config.Exchange,
                _config.RoutingKey,
                ProcessaMensagem,
                stoppingToken);

            await Task.Delay(-1, stoppingToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("[WORKER][{Nome}][EXCEPTION]: {Exception}", NomeWorker, ex.ToString());
        }
    }

    private async Task ProcessaMensagem(UserCreatedEvent dados)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var message = scope.ServiceProvider.GetRequiredService<IUserCreatedMessage>();
            await message.Consumir(dados);
        }
        catch (Exception ex)
        {
            _logger.LogError("[WORKER][{Nome}][EXCEPTION]: {Exception}", NomeWorker, ex.ToString());
        }
    }
}
