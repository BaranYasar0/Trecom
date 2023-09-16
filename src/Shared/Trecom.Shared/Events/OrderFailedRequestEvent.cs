using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Shared.Events.Interfaces;

namespace Trecom.Shared.Events;

public class OrderFailedRequestEvent:IOrderFailedRequestEvent
{
    public Guid CorrelationId { get; }
    public Guid OrderId { get; set; }
    public List<string> Reasons { get; set; }

    public OrderFailedRequestEvent(Guid correlationId)
    {
        CorrelationId = correlationId;
    }

    public OrderFailedRequestEvent(Guid correlationId,Guid orderId ,List<string> reasons)
    {
        CorrelationId = correlationId;
        Reasons = reasons;
        OrderId = orderId;
    }
}