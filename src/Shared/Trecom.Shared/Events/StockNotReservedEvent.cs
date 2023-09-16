using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Shared.Events.Interfaces;

namespace Trecom.Shared.Events;

public class StockNotReservedEvent:IStockNotReservedEvent
{
    public Guid OrderId { get; set; }
    public string? Reason { get; set; }
    public Guid CorrelationId { get; }
        
    public StockNotReservedEvent(Guid orderId, string reason, Guid correlationId)
    {
        OrderId = orderId;
        Reason = reason;
        CorrelationId = correlationId;
    }

    public static StockNotReservedEvent Create(Guid correlationId, Guid orderId,string reason="Default reason")
    {
        return new(orderId,reason,correlationId);
    }

}