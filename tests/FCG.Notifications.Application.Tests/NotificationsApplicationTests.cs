using FCG.Notifications.Application.Messaging.Consumers;
using FCG.Notifications.Application.Messaging.Events;
using Microsoft.Extensions.Logging;
using Moq;

namespace FCG.Notifications.Application.Tests;

public class UserCreatedConsumerTests
{
    [Fact]
    public async Task Consumir_DeveConcluirSemErros()
    {
        var logger = new Mock<ILogger<UserCreatedConsumer>>();
        var consumer = new UserCreatedConsumer(logger.Object);
        var evento = new UserCreatedEvent(Guid.NewGuid(), "usuario@teste.com", "João", DateTime.UtcNow);

        var exception = await Record.ExceptionAsync(() => consumer.Consumir(evento));

        Assert.Null(exception);
    }
}

public class PaymentProcessedConsumerTests
{
    [Fact]
    public async Task Consumir_ComPagamentoAprovado_DeveConcluirSemErros()
    {
        var logger = new Mock<ILogger<PaymentProcessedConsumer>>();
        var consumer = new PaymentProcessedConsumer(logger.Object);
        var evento = new PaymentProcessedEvent(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Approved", DateTime.UtcNow);

        var exception = await Record.ExceptionAsync(() => consumer.Consumir(evento));

        Assert.Null(exception);
    }

    [Fact]
    public async Task Consumir_ComPagamentoRejeitado_DeveRetornarSemEnviarEmail()
    {
        var logger = new Mock<ILogger<PaymentProcessedConsumer>>();
        var consumer = new PaymentProcessedConsumer(logger.Object);
        var evento = new PaymentProcessedEvent(
            Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Rejected", DateTime.UtcNow);

        await consumer.Consumir(evento);

        logger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString()!.Contains("Pagamento não aprovado")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
