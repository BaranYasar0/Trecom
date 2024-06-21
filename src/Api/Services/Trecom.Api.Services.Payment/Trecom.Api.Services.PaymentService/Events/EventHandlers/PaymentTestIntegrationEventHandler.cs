using Trecom.Api.Services.PaymentService.Events.Events;
using Trecom.ServiceBus.BusinessAction.Domain;

namespace Trecom.Api.Services.PaymentService.Events.EventHandlers
{
    public class PaymentTestIntegrationEventHandler : IIntegrationEventHandler<PaymentTestIntegrationEvent>
    {
        public Task HandleEvent(PaymentTestIntegrationEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
