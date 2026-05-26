using FCG.Notifications.Application;
using FCG.Notifications.Infrastructure;
using FCG.Notifications.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.Configure<UserCreatedWorkerConfig>(
    builder.Configuration.GetSection("Workers:UserCreated"));
builder.Services.Configure<PaymentProcessedWorkerConfig>(
    builder.Configuration.GetSection("Workers:PaymentProcessed"));

builder.Services.AddHostedService<UserCreatedWorker>();
builder.Services.AddHostedService<PaymentProcessedWorker>();

var host = builder.Build();
host.Run();
