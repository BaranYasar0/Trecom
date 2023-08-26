using System.Security.Cryptography;
using MassTransit;
using Trecom.Shared.Events;
using Trecom.Shared.Events.Interfaces;

namespace Trecom.Api.Services.PaymentService.Consumers
{
    public class StockReservedEventConsumer : IConsumer<IPaymentStartedRequestEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<StockReservedEventConsumer> _logger;
        public StockReservedEventConsumer(IPublishEndpoint publishEndpoint, ILogger<StockReservedEventConsumer> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }


        public async Task Consume(ConsumeContext<IPaymentStartedRequestEvent> context)
        {
            int randomNum = RandomNumberGenerator.GetInt32(1, 3);

            try
            {
                if (randomNum > 1)
                    await _publishEndpoint.Publish(new PaymentCompletedEvent(context.Message.CorrelationId));

                else
                    await _publishEndpoint.Publish(new PaymentFailedEvent(context.Message.CorrelationId,"Balance is not enough",context.Message.OrderItems));
            }
            catch (Exception e)
            {
                _logger.LogInformation(e,$"{this.GetType().Name}'s exception thrown with correlationId:{context.CorrelationId}");
                throw;
            }
        }
    }
}
