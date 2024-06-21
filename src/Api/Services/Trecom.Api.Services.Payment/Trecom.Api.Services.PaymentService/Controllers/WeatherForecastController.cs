using Microsoft.AspNetCore.Mvc;
using Trecom.Api.Services.PaymentService.Events.Events;
using Trecom.ServiceBus.BusinessAction.Abstraction;

namespace Trecom.Api.Services.PaymentService.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IServiceBus serviceBus;
    public WeatherForecastController(ILogger<WeatherForecastController> logger, IServiceBus serviceBus)
    {
        _logger = logger;
        this.serviceBus = serviceBus;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {

        Task.Run(() =>
        {
            serviceBus.Publish("1", new PaymentTestIntegrationEvent { TestId = "1", CreatedBy = "brn" });
        }); 
        return Ok();
    }
}