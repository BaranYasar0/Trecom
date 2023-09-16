using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace Trecom.Shared.Events.Interfaces;

public interface IStockReservedEvent: CorrelatedBy<Guid>
{
    public Guid OrderId { get; set; }
    public PaymentMessage PaymentMessage { get; set; }
    public List<OrderItemMessage> OrderItemMessages { get; set; }
}