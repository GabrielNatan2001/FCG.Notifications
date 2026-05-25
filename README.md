# FCG.Notifications.Worker

Worker que consome eventos do RabbitMQ e simula envio de e-mails via log no console.

## Eventos consumidos

- `UserCreatedEvent` → e-mail de boas-vindas
- `PaymentProcessedEvent` (Approved) → confirmação de compra

## Executar

```bash
dotnet run --project src/FCG.Notifications.Worker
```

Requer RabbitMQ em `localhost:5672`.
