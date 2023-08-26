using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Shared.Events.Interfaces;

namespace Trecom.Shared.Events
{
    public class OrderCompletedRequestEvent:IOrderCompletedRequestEvent
    {
        public Guid CorrelationId { get; }
        public Guid OrderId { get; set; }

        public OrderCompletedRequestEvent(Guid correlationId, Guid orderId)
        {
            CorrelationId = correlationId;
            OrderId = orderId;
        }
    }
}
