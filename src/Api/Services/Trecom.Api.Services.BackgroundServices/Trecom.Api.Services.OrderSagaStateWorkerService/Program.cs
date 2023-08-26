using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Trecom.Api.Services.OrderSagaStateWorkerService;
using Trecom.Api.Services.OrderSagaStateWorkerService.Models;
using Trecom.Shared;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostcontext,services) =>
    {

        services.AddMassTransit(cfg =>
        {
            cfg.AddSagaStateMachine<OrderStateMachine, OrderStateInstance>().EntityFrameworkRepository(opt =>
            {
                opt.ConcurrencyMode = ConcurrencyMode.Pessimistic;

                opt.AddDbContext<DbContext, OrderStateDbContext>((provider, builder) =>
                {
                    builder.UseSqlServer(hostcontext.Configuration.GetConnectionString("SqlCon"), m =>
                    {
                        m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                        m.MigrationsHistoryTable($"__{nameof(OrderStateDbContext)}");
                    });
                });
            });

            cfg.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(configure =>
            {
                configure.Host(hostcontext.Configuration.GetConnectionString("RabbitMQ"));

                configure.ReceiveEndpoint(RabbitMqSettings.OrderSaga, e =>
                {
                    e.ConfigureSaga<OrderStateInstance>(provider);
                });

            }));

        });





        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
