namespace FCG.Notifications.Function.Models;

public sealed class PaymentProcessedEvent
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public Guid GameId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime ProcessedAtUtc { get; set; }
}
