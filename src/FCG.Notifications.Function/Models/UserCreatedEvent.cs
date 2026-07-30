namespace FCG.Notifications.Function.Models;

public sealed class UserCreatedEvent
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
}
