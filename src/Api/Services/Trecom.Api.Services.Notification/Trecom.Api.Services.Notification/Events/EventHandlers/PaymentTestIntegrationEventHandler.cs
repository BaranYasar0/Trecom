using Trecom.Api.Services.Notification.Events.Events;
using Trecom.ServiceBus.BusinessAction.Domain;

namespace Trecom.Api.Services.Notification.Events.EventHandlers
{
    public class PaymentTestIntegrationEventHandler : IIntegrationEventHandler<PaymentTestIntegrationEvent>
    {
        public Task HandleEvent(PaymentTestIntegrationEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
