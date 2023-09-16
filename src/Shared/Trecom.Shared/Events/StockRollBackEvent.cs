using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Shared.Events.Interfaces;

namespace Trecom.Shared.Events;

public class StockRollBackEvent:IStockRollBackEvent
{
    public Guid CorrelationId { get; }
    public List<OrderItemMessage> OrderItems { get; set; }

    public StockRollBackEvent(Guid correlationId, List<OrderItemMessage> orderItems)
    {
        CorrelationId = correlationId;
        OrderItems = orderItems;
    }
}