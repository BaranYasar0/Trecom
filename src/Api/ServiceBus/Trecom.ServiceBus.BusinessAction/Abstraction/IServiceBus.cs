using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.ServiceBus.Domain;

namespace Trecom.ServiceBus.BusinessAction.Abstraction
{
    public interface IServiceBus
    {
        void Publish(IntegrationEvent @event);
        void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        void UnSubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        void CallEsbService<T>(T service);
    }
}
