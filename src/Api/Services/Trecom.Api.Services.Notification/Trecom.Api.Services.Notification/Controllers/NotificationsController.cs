using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trecom.ServiceBus.BusinessAction.Abstraction;

namespace Trecom.Api.Services.Notification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController(IServiceBus serviceBus) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            //Task.Run(() => serviceBus.Publish("2",new PaymentTest))
            return Ok("Notification Service is running");
        }
    }
}
