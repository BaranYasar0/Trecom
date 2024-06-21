using MassTransit;
using Microsoft.EntityFrameworkCore;
using Trecom.Api.Services.Notification.Events.EventHandlers;
using Trecom.Api.Services.Notification.Events.Events;
using Trecom.Api.Services.Notification.Persistance;
using Trecom.ServiceBus.BusinessAction.Abstraction;
using Trecom.ServiceBus.BusinessAction.Domain;
using Trecom.ServiceBus.BusinessAction.EventManagers;
using Trecom.ServiceBus.Kafka;
using Trecom.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NotificationDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration["SqlCon"]);
});
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMqSettings:Url"]);
    });
});

builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));
builder.Services.Configure<ServiceBusConfig>(builder.Configuration.GetSection("ServiceBusConfig"));
builder.Services.AddScoped<PaymentTestIntegrationEventHandler>();
builder.Services.AddSingleton<IEventManager, InMemoryEventManager>();
builder.Services.AddScoped<IServiceBus, KafkaServiceBus>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using var scope = app.Services.CreateScope();
var serviceBus = scope.ServiceProvider.GetRequiredService<IServiceBus>();
await serviceBus.Subscribe<PaymentTestIntegrationEvent, PaymentTestIntegrationEventHandler>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
