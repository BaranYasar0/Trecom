using System.Net;
using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Trecom.Api.Services.Catalog.Application.Features.Commands;
using Trecom.Api.Services.Catalog.Application.Features.Queries;
using Trecom.Api.Services.Catalog.Models.Dtos;
using Trecom.Api.Services.Catalog.Models.Entities;
using Trecom.Api.Services.Catalog.Models.ViewModels;
using Trecom.Api.Services.Catalog.Persistance.EntityFramework;
using Trecom.Api.Services.Catalog.Persistance.Repository;

namespace Trecom.Api.Services.Catalog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : BaseController
{
        

    [HttpGet]
    [Route("getList")]
    [ProducesResponseType(type: typeof(PaginationViewModel<ProductResponseDto>), statusCode: 200)]
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllAsync([FromQuery] QueryPaginationDto pagination)
    {
        return Ok(await MediatR.Send(new GetProductListQuery(pagination)));
    }

    [HttpGet]
    [Route("getById/{id:guid}")]
    [ProducesResponseType(type: typeof(PaginationViewModel<ProductResponseDto>), statusCode: 200)]
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        return Ok(await MediatR.Send(new GetProductByIdQuery(id)));
    }

    [HttpGet]
    [Route("getListByIds")]
    [ProducesResponseType(type: typeof(PaginationViewModel<ProductResponseDto>), statusCode: 200)]
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetListByIdsAsync([FromQuery] string name)
    {
        //var result = await client.SearchAsync<Product>(s => s.Index("catalog")
        //    .Query(q =>
        //        q.Range(t =>
        //            t.NumberRange(f =>
        //                f.Field(g=>g.UnitPrice)));

        //var result = await client.SearchAsync<Product>(s => s.Index("catalog")
        //               .Query(q => q.MatchAll()));

        return Ok(/*result.Documents.ToList()*/);
    } 

    [HttpGet]
    [Route("getByName")]
    [ProducesResponseType(type: typeof(PaginationViewModel<ProductResponseDto>), statusCode: 200)]
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetListByIdsAsync([FromQuery] QueryPaginationDto pagination, [FromQuery] string name)
    {
        return Ok(await MediatR.Send(new GetProductListByNameQuery(name, pagination)));
    }

    [HttpGet]
    [Route("getListByCategoryName")]
    [ProducesResponseType(type: typeof(PaginationViewModel<ProductResponseDto>), statusCode: 200)]
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetListByCategoryNameAsync([FromQuery] QueryPaginationDto pagination, [FromQuery] string categoryName)
    {
        return Ok(await MediatR.Send(new GetProductListByCategoryNameQuery(categoryName, pagination)));
    }

    [HttpGet]
    [Route("getListByBrandId/{id:guid}")]
    [ProducesResponseType(type: typeof(PaginationViewModel<ProductResponseDto>), statusCode: 200)]
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetListByBrandIdAsync([FromQuery] QueryPaginationDto pagination, [FromRoute] Guid id)
    {
        return Ok(await MediatR.Send(new GetProductListByBrandIdQuery(id, pagination)));
    }

    [HttpGet]
    [Route("getListBySupplierId/{id:guid}")]
    [ProducesResponseType(type: typeof(PaginationViewModel<ProductResponseDto>), statusCode: 200)]
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetListByIdsAsync([FromQuery] QueryPaginationDto pagination, [FromRoute] Guid id)
    {
        return Ok(await MediatR.Send(new GetProductListBySupplierIdQuery(id, pagination)));
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(type: typeof(CreateProductResponseDto), statusCode: 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProductCommand command)
    {
        return Ok(await MediatR.Send(command));
    }

        
}