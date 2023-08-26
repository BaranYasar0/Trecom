using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace Trecom.Shared.Events.Interfaces
{
    public interface IOrderFailedRequestEvent:CorrelatedBy<Guid>
    {
       public Guid OrderId { get; set; }
        public List<string> Reasons { get; set; }
    }
}
