using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trecom.Services.Api.Stock.Models;

namespace Trecom.Services.Api.Stock.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StocksController : ControllerBase
{
    private readonly StockDbContext context;

    public StocksController(StockDbContext context)
    {
        this.context = context;
    }

    [HttpGet("{productId:guid}")]
    [ProducesResponseType(type:typeof(int),statusCode:200)]
    [ProducesResponseType(statusCode:StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetStockByProductId([FromRoute] Guid productId)
    {
        return Ok(await context.Stocks.Where(x=>x.ProductId==productId).Select(y=>y.Amount).ToListAsync());
    }
}