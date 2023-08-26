using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Shared.Events.Interfaces;

namespace Trecom.Shared.Events
{
    public class BasketCheckOutEvent:IBasketCheckOutEvent
    {
        public List<OrderItemMessage> OrderItemMessages { get; set; }
        public PaymentMessage PaymentMessage { get; set; }
        public Guid UserId { get; set; }
        public AddressMessage AddressMessage { get; set; }


        public BasketCheckOutEvent(List<OrderItemMessage> orderItemMessages, PaymentMessage paymentMessage, Guid userId, AddressMessage addressMessage)
        {
            OrderItemMessages = orderItemMessages;
            PaymentMessage = paymentMessage;
            UserId = userId;
            AddressMessage = addressMessage;
        }
    }
}
