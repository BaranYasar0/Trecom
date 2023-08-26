using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace Trecom.Shared.Events.Interfaces
{
    public interface IPaymentStartedRequestEvent:CorrelatedBy<Guid>
    {
        public string CardNumber { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; }
    }
}
