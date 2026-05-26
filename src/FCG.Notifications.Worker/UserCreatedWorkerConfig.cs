namespace FCG.Notifications.Worker;

public class UserCreatedWorkerConfig
{
    public bool Ativo { get; set; } = true;
    public string Exchange { get; set; } = string.Empty;
    public string RoutingKey { get; set; } = string.Empty;
}
