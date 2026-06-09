using FCG.Notifications.Application;
using FCG.Notifications.Infrastructure;
using FCG.Notifications.Worker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHealthChecksInfrastructure(builder.Configuration);

builder.Services.Configure<UserCreatedWorkerConfig>(
    builder.Configuration.GetSection("Workers:UserCreated"));
builder.Services.Configure<PaymentProcessedWorkerConfig>(
    builder.Configuration.GetSection("Workers:PaymentProcessed"));

builder.Services.AddHostedService<UserCreatedWorker>();
builder.Services.AddHostedService<PaymentProcessedWorker>();

var app = builder.Build();

app.MapHealthChecks("/health");
app.Run();
