using Trecom.ServiceBus.BusinessAction.Abstraction;
using Trecom.ServiceBus.BusinessAction.Domain;

namespace Trecom.Api.Services.Notification.Events.Events
{
    public class PaymentTestIntegrationEvent : IntegrationEvent
    {
        public string TestId { get; set; }

    }
}
