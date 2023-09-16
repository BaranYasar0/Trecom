using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace Trecom.Shared.Events.Interfaces;

public interface IPaymentFailedEvent:CorrelatedBy<Guid>
{
    public string Reason { get; set; }
    public List<OrderItemMessage> orderItems { get; set; }
}