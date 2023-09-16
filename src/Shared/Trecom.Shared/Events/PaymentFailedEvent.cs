using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Shared.Events.Interfaces;

namespace Trecom.Shared.Events;

public class PaymentFailedEvent:IPaymentFailedEvent
{
    public Guid CorrelationId { get; }
    public string Reason { get; set; }
    public List<OrderItemMessage> orderItems { get; set; }

    public PaymentFailedEvent(Guid correlationId, string reason, List<OrderItemMessage> orderItems)
    {
        CorrelationId = correlationId;
        Reason = reason;
        this.orderItems = orderItems;
    }
}