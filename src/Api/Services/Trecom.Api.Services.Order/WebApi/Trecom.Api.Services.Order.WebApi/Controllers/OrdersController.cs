using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trecom.Api.Services.Order.Application.Features.Commands;

namespace Trecom.Api.Services.Order.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrdersController : BaseController
{

    [HttpPost]
    public async Task<IActionResult> Add(CreateOrderCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
        
        
}