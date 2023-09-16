using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trecom.Shared.Events.Interfaces;

public interface IBasketCheckOutEvent
{
    public List<OrderItemMessage> OrderItemMessages { get; set; }
    public PaymentMessage PaymentMessage { get; set; }
    public Guid UserId { get; set; }
    public AddressMessage AddressMessage { get; set; }

}