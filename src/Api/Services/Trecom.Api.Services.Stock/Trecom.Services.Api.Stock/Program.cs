using MassTransit;
using Microsoft.EntityFrameworkCore;
using Trecom.Services.Api.Stock.Consumers;
using Trecom.Services.Api.Stock.Models;
using Trecom.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StockDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<StockRollBackEventConsumer>();
    x.AddConsumer<OrderCreatedEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQSettings"]);

        cfg.ReceiveEndpoint(RabbitMqSettings.OrderCreatedEventQueueName, con =>
        {
            con.ConfigureConsumer<OrderCreatedEventConsumer>(context);
        });

        cfg.ReceiveEndpoint(RabbitMqSettings.StockRollBackEventQueueName, con =>
        {
            con.ConfigureConsumer<StockRollBackEventConsumer>(context);
        });
    });
});

var app = builder.Build();


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
