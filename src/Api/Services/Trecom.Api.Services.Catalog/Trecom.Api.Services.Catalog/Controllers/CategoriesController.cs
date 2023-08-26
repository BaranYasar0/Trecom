using Microsoft.AspNetCore.Mvc;
using Trecom.Api.Services.Catalog.Application.Features.Queries;
using Trecom.Api.Services.Catalog.Models.Dtos;

namespace Trecom.Api.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetBaseCategories([FromQuery] QueryPaginationDto pagination)
        {
            var query = new GetBaseCategoryListQuery(pagination);
            var result = await MediatR.Send(query);
            return Ok(result);
        }
    }
}
