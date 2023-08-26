using MassTransit;
using Trecom.Api.Services.PaymentService.Consumers;
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
