using System.Text;
using MassTransit;
using Trecom.Api.Identity.Application.Events;
using Trecom.Shared;

namespace Trecom.Api.Identity.Application.Observers.User
{
    public class UserSendEmail : IUserObserver
    {
        private readonly IServiceProvider sp;

        public UserSendEmail(IServiceProvider sp)
        {
            this.sp = sp;
        }

        public async Task UserCreatedAsync(Models.Entities.User user)
        {
            var logger = sp.GetRequiredService<ILogger<UserSendEmail>>();
            var sendEndpointProvider = sp.GetRequiredService<ISendEndpointProvider>();
            
            try
            {
                StringBuilder sb = new();

                sb.Append($"{user.FirstName}").Append(" ").Append($"{user.LastName}");

                var sender=await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMqSettings.UserCreatedMailQueueName}"));
                
                await sender.Send(UserCreatedEvent.Create(user.Email, sb.ToString()));
                
            }
            catch (Exception e)
            {
                logger.LogInformation($"Mail gönderilemedi",e);
                throw;
            }
            
            logger.LogInformation($"{nameof(UserCreatedEvent)} published t {user.Email} for account creation");

        }
    }
}
