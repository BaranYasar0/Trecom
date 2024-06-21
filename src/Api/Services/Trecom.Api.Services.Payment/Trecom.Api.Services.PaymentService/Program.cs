using MassTransit;
using Trecom.Api.Services.PaymentService;
using Trecom.Api.Services.PaymentService.Consumers;
using Trecom.Api.Services.PaymentService.Events.EventHandlers;
using Trecom.Api.Services.PaymentService.Events.Events;
using Trecom.ServiceBus.BusinessAction.Abstraction;
using Trecom.ServiceBus.BusinessAction.Domain;
using Trecom.ServiceBus.BusinessAction.EventManagers;
using Trecom.ServiceBus.Kafka;
using Trecom.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(cfg =>
{
    cfg.AddConsumer<StockReservedEventConsumer>();
    cfg.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["RabbitMQSettings"]);

        config.ReceiveEndpoint(RabbitMqSettings.StockReservedQueueName, c =>
        {
            c.ConfigureConsumer<StockReservedEventConsumer>(context);
        });
    });

});

builder.Services.Configure<ServiceBusConfig>(builder.Configuration.GetSection("ServiceBusConfig"));
builder.Services.AddScoped<PaymentTestIntegrationEventHandler>();
builder.Services.AddSingleton<IEventManager, InMemoryEventManager>();
builder.Services.AddScoped<IServiceBus, KafkaServiceBus>();


var app = builder.Build();

using var scope = app.Services.CreateScope();

var serviceBus = scope.ServiceProvider.GetRequiredService<IServiceBus>();
await serviceBus.Subscribe<PaymentTestIntegrationEvent, PaymentTestIntegrationEventHandler>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
