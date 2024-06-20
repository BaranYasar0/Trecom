using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Trecom.ServiceBus.BusinessAction.EventManagers;
using Trecom.ServiceBus.Domain;

namespace Trecom.ServiceBus.BusinessAction.Abstraction
{
    public abstract class BaseServiceBus : IServiceBus, IDisposable
    {
        public abstract void Publish<TEvent>(string key, TEvent @event) where TEvent : IntegrationEvent;
        public abstract Task Subscribe<T, TH>(CancellationToken cancellationToken = default) where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        public abstract void UnSubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        public abstract void CallEsbService<T>(T message);

        public readonly IEventManager eventManager;
        public readonly IServiceProvider serviceProvider;
        public ServiceBusConfig config;
        public BaseServiceBus(ServiceBusConfig config, IEventManager eventManager, IServiceProvider serviceProvider)
        {
            this.eventManager = eventManager;
            this.config = config;
            this.serviceProvider = serviceProvider;
        }

        public virtual async Task<bool> ProcessEvent(string eventName,string eventMessage)
        {
            var processed = false;

            if (eventManager.HasSubscriptionsForEvent(eventName))
            {
                IEnumerable<SubscriptionInfo> subscriptions = eventManager.GetHandlersForEvent(eventName);
                
                foreach (SubscriptionInfo subscription in subscriptions)
                {
                    Type handlerType = subscription.HandlerType;
                    object handler = serviceProvider.GetService(handlerType);
                    if (handler == null) continue;

                    Type eventType = eventManager.GetEventTypeByName(eventName);
                    object integrationEvent = JsonConvert.DeserializeObject(eventMessage,eventType);
                    Type concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                    await (Task)concreteType.GetMethod("Handle").Invoke(handler,new object[] { integrationEvent });
                    processed = true;
                }
            }

            return processed;
        }

        public virtual void Dispose()
        {
            config = null;
            eventManager.Clear();
        }
    }
}
