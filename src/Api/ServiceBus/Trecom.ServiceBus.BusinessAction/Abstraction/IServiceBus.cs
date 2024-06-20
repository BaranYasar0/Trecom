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
        void Publish<TEvent>(string key, TEvent @event) where TEvent : IntegrationEvent;
        Task Subscribe<T, TH>(CancellationToken cancellationToken = default) where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        void UnSubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        void CallEsbService<T>(T service);
    }
}
