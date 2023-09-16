using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Trecom.Api.Services.Catalog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected IMediator? MediatR => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    private IMediator? _mediator;
}