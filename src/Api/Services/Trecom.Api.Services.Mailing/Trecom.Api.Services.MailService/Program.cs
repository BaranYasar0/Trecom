using MassTransit;
using Microsoft.EntityFrameworkCore;
using Trecom.Api.Services.MailService.Consumers;
using Trecom.Api.Services.MailService.Models;
using Trecom.Api.Services.MailService.Services;
using Trecom.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MailDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration["SqlCon"]);
});

builder.Services.Configure<EmailConfiguration>(
    builder.Configuration.GetSection("EmailConfiguration")
);
builder.Services.AddScoped<IMailService, MailService>();

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<UserCreatedEventConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(builder.Configuration["RabbitMqSettings:Host"]));

        cfg.ReceiveEndpoint(RabbitMqSettings.UserCreatedMailQueueName, ce =>
        {
            ce.ConfigureConsumer<UserCreatedEventConsumer>(context);
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
