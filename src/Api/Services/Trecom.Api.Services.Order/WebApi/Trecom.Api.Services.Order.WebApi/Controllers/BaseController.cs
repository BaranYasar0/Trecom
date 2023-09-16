using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Trecom.Api.Services.Order.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected IMediator? Mediator => _mediator ??= HttpContext?.RequestServices?.GetService<IMediator>();
    protected IMediator? _mediator;
}