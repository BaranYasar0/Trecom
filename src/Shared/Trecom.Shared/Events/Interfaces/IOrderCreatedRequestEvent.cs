using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trecom.Shared.Events.Interfaces
{
    public interface IOrderCreatedRequestEvent
    {
        public Guid OrderId { get; set; }
        public Guid BuyerId { get; set; }
        public PaymentMessage PaymentMessage { get; set; }
        public List<OrderItemMessage> OrderItemMessages { get; set; }
    }
}
