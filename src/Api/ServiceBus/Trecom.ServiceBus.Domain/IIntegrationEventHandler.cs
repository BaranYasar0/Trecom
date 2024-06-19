using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trecom.ServiceBus.Domain
{
    public interface IIntegrationEventHandler<TEvent> where TEvent : IntegrationEvent
    {
        Task HandleEvent(TEvent @event);
    }


}
