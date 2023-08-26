using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trecom.Api.Identity.Application.Features.Commands.Update;
using Trecom.Api.Identity.Application.Features.Queries.GetAll;
using Trecom.Api.Identity.Application.Features.Queries.GetById;

namespace Trecom.Api.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
        {
            var query = new GetUserByIdQuery() { Id = userId };
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            GetAllUsersQuery query = new GetAllUsersQuery();

            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        [Route("ConfigureUserClaims")]
        public async Task<IActionResult> ConfigureUserClaims([FromBody] ConfigureUserOperationClaimCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
