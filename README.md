# FCG.Notifications

Worker responsável por **simular o envio de e-mails** via log no console. Consome eventos do RabbitMQ e registra as notificações que seriam disparadas em produção.

## Projetos

| Projeto | Descrição |
|---|---|
| `FCG.Notifications.Worker` | Consumers de eventos de usuário e pagamento |
| `FCG.Notifications.Application` | Handlers de notificação |
| `FCG.Notifications.Infrastructure` | Integração com RabbitMQ |

## Imagem Docker

`gabrielnatan2001/fcg-worker-notifications:latest`

## Eventos consumidos

| Evento | Ação simulada |
|---|---|
| `UserCreatedEvent` | E-mail de boas-vindas |
| `PaymentProcessedEvent` (Approved) | Confirmação de compra |

## Variáveis de ambiente

| Variável (Docker/K8s) | appsettings | Obrigatória | Descrição | Exemplo |
|---|---|---|---|---|
| `ASPNETCORE_ENVIRONMENT` | — | Sim | Ambiente de execução | `Production` |
| `MessageBusConfigs__Host` | `MessageBusConfigs:Host` | Sim | URI do RabbitMQ | `amqp://admin:admin@rabbitmq:5672/` |
| `MessageBusConfigs__RetryCount` | `MessageBusConfigs:RetryCount` | Não | Tentativas de reconexão | `5` |
| `Workers__UserCreated__Ativo` | `Workers:UserCreated:Ativo` | Sim | Habilita consumer de usuário criado | `true` |
| `Workers__UserCreated__Exchange` | `Workers:UserCreated:Exchange` | Sim | Exchange do evento | `fcg.user.created` |
| `Workers__UserCreated__RoutingKey` | `Workers:UserCreated:RoutingKey` | Sim | Routing key do evento | `notifications.user-created` |
| `Workers__PaymentProcessed__Ativo` | `Workers:PaymentProcessed:Ativo` | Sim | Habilita consumer de pagamento | `true` |
| `Workers__PaymentProcessed__Exchange` | `Workers:PaymentProcessed:Exchange` | Sim | Exchange do pagamento | `fcg.payment.processed` |
| `Workers__PaymentProcessed__RoutingKey` | `Workers:PaymentProcessed:RoutingKey` | Sim | Routing key do pagamento | `notifications.payment-processed` |

> Este serviço **não utiliza banco de dados**.

## Executar localmente

```bash
dotnet run --project src/FCG.Notifications.Worker
```

Requer RabbitMQ acessível (padrão local: `localhost:5672`). Para subir a stack completa, use o [FCG.Infra](../FCG.Infra/README.md).

## Deploy

Manifests Kubernetes em `k8s/`. Instruções completas no [README do FCG.Infra](../FCG.Infra/README.md).
