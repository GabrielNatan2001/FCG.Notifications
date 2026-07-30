# FCG.Notifications

Microsserviço de notificações da FCG. Na Fase 3, o processamento foi migrado do
worker 24/7 para uma **Azure Function (.NET 8 Isolated)** acionada pelo RabbitMQ.

## Projetos

| Projeto | Descrição |
|---|---|
| `FCG.Notifications.Function` | Implementação serverless utilizada na Fase 3 |
| `FCG.Notifications.Application` | Contratos, eventos e lógica de notificação |
| `FCG.Notifications.Infrastructure` | Integração legada com RabbitMQ |
| `FCG.Notifications.Worker` | Worker legado da Fase 2; não deve ser implantado |

## Eventos da Function

| Fila | Evento | Ação |
|---|---|---|
| `notifications.user-created-queue` | `UserCreatedEvent` | Simula e-mail de boas-vindas |
| `notifications.payment-processed-queue` | `PaymentProcessedEvent` aprovado | Simula confirmação de compra |

As filas seguem a convenção `{routingKey}-queue` e são provisionadas pelo
`FCG.Infra/rabbitmq/definitions.json`.

## Executar localmente

Pré-requisitos:

- .NET 8 SDK
- Azure Functions Core Tools v4
- RabbitMQ acessível

```bash
cd src/FCG.Notifications.Function
cp local.settings.json.example local.settings.json
# Ajuste a conexão RabbitMQ no arquivo local
func start
```

## Deploy serverless

O arquivo [`infra/azure-function/main.bicep`](infra/azure-function/main.bicep)
provisiona Storage Account, Consumption Plan e Function App.

```bash
az group create -n fcg-notifications-rg -l brazilsouth
az deployment group create \
  -g fcg-notifications-rg \
  -f infra/azure-function/main.bicep \
  --parameters rabbitMqConnectionString='amqp://admin:admin@<host>:5672/'

func azure functionapp publish fcg-notifications-func
```

O RabbitMQ precisa ser acessível pela Function hospedada no Azure.
