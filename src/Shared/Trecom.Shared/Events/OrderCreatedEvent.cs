using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Newtonsoft.Json;
using Trecom.Shared.Events.Interfaces;

namespace Trecom.Shared.Events
{
    public class OrderCreatedEvent : IOrderCreatedEvent
    {

        public List<OrderItemMessage> OrderItemMessages { get; set; } = new List<OrderItemMessage>();
        public decimal TotalPrice
        {
            get => OrderItemMessages != null ? OrderItemMessages.Sum(x => x.Price * x.Quantity) : 0;
        }

        [JsonConstructor]
        public OrderCreatedEvent()
        {

        }

        public OrderCreatedEvent(Guid correlationId, List<OrderItemMessage> orderItem)
        {
            CorrelationId = correlationId;
            OrderItemMessages = orderItem;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} has been created";
        }

        public Guid CorrelationId { get; }
    }

    public interface IOrderCreatedEvent : CorrelatedBy<Guid>
    {
        public List<OrderItemMessage> OrderItemMessages { get; set; }
    }
}
