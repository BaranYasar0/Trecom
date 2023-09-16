using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Shared.Events.Interfaces;

namespace Trecom.Shared.Events;

public class StockReservedEvent:IStockReservedEvent
{
    public Guid CorrelationId { get; }
    public Guid OrderId { get; set; }
    public PaymentMessage PaymentMessage { get; set; }
    public List<OrderItemMessage> OrderItemMessages { get; set; }

    public StockReservedEvent(Guid correlationId, Guid orderId, PaymentMessage paymentMessage, List<OrderItemMessage> orderItemMessages)
    {
        CorrelationId = correlationId;
        OrderId = orderId;
        PaymentMessage = paymentMessage;
        OrderItemMessages = orderItemMessages;
    }

    public static StockReservedEvent Create(Guid correlationId, Guid orderId, PaymentMessage paymentMessage,
        List<OrderItemMessage> orderItemMessages)
    {
        return new(correlationId, orderId, paymentMessage, orderItemMessages);
    }
}