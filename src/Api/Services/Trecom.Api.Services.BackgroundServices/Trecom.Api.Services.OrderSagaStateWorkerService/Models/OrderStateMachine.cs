using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Trecom.Shared;
using Trecom.Shared.Events;
using Trecom.Shared.Events.Interfaces;

namespace Trecom.Api.Services.OrderSagaStateWorkerService.Models;

public class OrderStateMachine : MassTransitStateMachine<OrderStateInstance>
{
    //private readonly IMapper mapper;


    public Event<IOrderCreatedRequestEvent> OrderCreatedRequestEvent { get; set; }
    public Event<IOrderFailedRequestEvent> OrderFailedRequestEvent { get; set; }
    public Event<IOrderCompletedRequestEvent> OrderCompletedRequestEvent { get; set; }
    public Event<IStockReservedEvent> StockReservedEvent { get; set; }
    public Event<IStockNotReservedEvent> StockNotReservedEvent { get; set; }
    public Event<IPaymentStartedRequestEvent> PaymenStartedRequestEvent { get; set; }
    public Event<IPaymentCompletedEvent> PaymentCompletedEvent { get; set; }
    public Event<IPaymentFailedEvent> PaymentFailedEvent { get; set; }

    public State OrderCreated { get; set; }
    public State StockReserved { get; set; }
    public State StockNotReserved { get; set; }
    public State PaymentCompleted { get; set; }
    public State PaymentFailed { get; set; }

    public OrderStateMachine()
    {

        InstanceState(x => x.CurrentState);

        Event(() => OrderCreatedRequestEvent, x => x.CorrelateBy<Guid>(y => y.OrderId, z => z.Message.OrderId)
            .SelectId(s => Guid.NewGuid()));
        Event(() => StockReservedEvent, x => x.CorrelateById(c => c.Message.CorrelationId));
        Event(() => StockNotReservedEvent, x => x.CorrelateById(c => c.Message.CorrelationId));
        Event(() => PaymenStartedRequestEvent, x => x.CorrelateById(c => c.Message.CorrelationId));
        Event(() => PaymentCompletedEvent, x => x.CorrelateById(c => c.Message.CorrelationId));
        Event(() => PaymentFailedEvent, x => x.CorrelateById(c => c.Message.CorrelationId));
        Event(() => OrderCompletedRequestEvent, x => x.CorrelateById(c => c.Message.CorrelationId));
        Event(() => OrderFailedRequestEvent, x => x.CorrelateById(c => c.Message.CorrelationId));

        Initially(When(OrderCreatedRequestEvent)
            .ThenAsync(async context =>
            {
                context.Saga.OrderId = context.Message.OrderId;
                context.Saga.BuyerId = context.Message.BuyerId;
                context.Saga.CreatedDate = DateTime.Now;
                context.Saga.CardNumber = context.Message.PaymentMessage?.CardNumber;
            }).ThenAsync(async context =>
            {
                Console.WriteLine($"OrderCreatedEvent was handled : {context.Saga}");
            })
            .Publish(context => new OrderCreatedEvent(context.Saga.CorrelationId, context.Message.OrderItemMessages))
            .ThenAsync(async context =>
            {
                Console.WriteLine($"OrderCreatedEvent published : {context.Saga}");
            })
            .TransitionTo(OrderCreated)
        );

        During(OrderCreated,
            When(StockNotReservedEvent)
                .ThenAsync(async context =>
                {
                    Console.WriteLine($"{context.Event.Name} was handled : {context.Saga}");
                })
                .Send(new Uri($"queue:{RabbitMqSettings.OrderFailedQueueName}"), context => new OrderFailedRequestEvent(context.Saga.CorrelationId,context.Message.OrderId,
                    new() { new string("Stock not reserved") }))
                .ThenAsync(async context =>
                {
                    Console.WriteLine($"OrderNotCompleted event sent to Order Service with corId:{context.Saga.CorrelationId}");
                })
                .TransitionTo(StockNotReserved)

            ,
            When(StockReservedEvent)
                .TransitionTo(StockReserved)
                .ThenAsync(async context =>
                {
                    Console.WriteLine($"{context.Event.Name} was handled");
                    Console.WriteLine($"{context.Event.Name} before:{context.Saga}");
                })
                .PublishAsync(async context => new PaymentStartedRequestEvent(context.Saga.CorrelationId, context.Saga.CardNumber, context.Saga.TotalPrice,context.Message.OrderItemMessages))
                .ThenAsync(async context =>
                {
                    Console.WriteLine($"{context.Event.Name} published");
                    Console.WriteLine($"{context.Event.Name} after:{context.Saga}");
                })
        );


        During(StockReserved,
            When(PaymentCompletedEvent)
                .TransitionTo(PaymentCompleted)
                .ThenAsync(async context =>
                {
                    Console.WriteLine($"{context.Event.Name} was handled : {context.Saga}");
                })
                .PublishAsync(async context => new OrderCompletedRequestEvent(context.Saga.CorrelationId, context.Saga.OrderId))
                .ThenAsync(async context =>
                {
                    Console.WriteLine($"OrderCompletedRequestEvent published ");
                    Console.WriteLine($"Last situtation:{context.Saga}");
                })
                .Finalize()
            ,
            When(PaymentFailedEvent)
                .TransitionTo(PaymentFailed)
                .ThenAsync(async context =>
                {
                    Console.WriteLine($"{context.Event.Name} was handled after: {context.Saga}");
                })
                .SendAsync(new Uri($"queue:"),async context=>new StockRollBackEvent(context.Message.CorrelationId,context.Message.orderItems))
                .ThenAsync(async context =>
                {
                    Console.WriteLine($"StockRollBack event sended to Stock Service and after the Saga Instance is : {context.Saga}");
                })
                .PublishAsync(async context=>new OrderFailedRequestEvent(context.Message.CorrelationId,context.Saga.OrderId,new List<string>(){new string("Not Enough Balance.")}))
                .ThenAsync(async context =>
                {
                    Console.WriteLine($"OrderFailedRequestEvent published");
                    Console.WriteLine($"Last situtation:{context.Saga}");
                })
        );



    }
}