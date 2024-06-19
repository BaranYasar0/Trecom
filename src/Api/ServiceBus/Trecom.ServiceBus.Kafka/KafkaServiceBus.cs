using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.ServiceBus.BusinessAction.Abstraction;
using Trecom.ServiceBus.Domain;

namespace Trecom.ServiceBus.Kafka;

public class KafkaServiceBus : BaseServiceBus
{
    public ServiceBusConfig config;

    public KafkaServiceBus(ServiceBusConfig config, IEventManager eventManager, IServiceProvider serviceProvider) : base(config, eventManager, serviceProvider)
    {
    }

    public override void CallEsbService<T>(T message)
    {
        //Do nothing
    }

    public override void Publish(IntegrationEvent @event)
    {
        throw new NotImplementedException();
    }

    public override void Subscribe<T, TH>()
    {
        throw new NotImplementedException();
    }

    public override void UnSubscribe<T, TH>()
    {
        throw new NotImplementedException();
    }

    #region Private Methods


    #endregion
}