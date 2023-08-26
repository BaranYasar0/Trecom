using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Shared.Events.Interfaces;

namespace Trecom.Shared.Events
{
    public class PaymentStartedRequestEvent: IPaymentStartedRequestEvent
    {
        public Guid CorrelationId { get; }
        public string CardNumber { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; }
        public decimal TotalPrice { get; set; }

        public PaymentStartedRequestEvent(Guid correlationId, string cardNumber, decimal totalPrice, List<OrderItemMessage> orderItems)
        {
            CorrelationId = correlationId;
            CardNumber = cardNumber;
            TotalPrice = totalPrice;
            OrderItems = orderItems;
        }
    }
}
