using Trecom.ServiceBus.BusinessAction.Abstraction;
using Trecom.ServiceBus.BusinessAction.Domain;
using Trecom.ServiceBus.Kafka;

namespace Trecom.Api.Services.PaymentService.Events.Events
{
    public class PaymentTestIntegrationEvent:IntegrationEvent
    {
        public string TestId { get; set; }

    }
}
