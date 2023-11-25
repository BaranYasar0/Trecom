using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trecom.Api.Services.BasketService.Models;
using Trecom.Api.Services.BasketService.Persistance;

namespace Trecom.Api.Services.BasketService.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BasketsController : ControllerBase
{

    BasketRepository basketRepository;

    public BasketsController(BasketRepository basketRepository)
    {
        this.basketRepository = basketRepository;
    }

    [HttpGet("getBasket")]
    public async Task<IActionResult> GetBasketAsync()
    {
        var basket = await basketRepository.GetBasketAsync();
        return Ok(basket);
    }

    [HttpPost("addBasketItem")]
    public async Task<IActionResult> AddBasketItemAsync([FromBody]BasketItem basketItem)
    {
        var basket = await basketRepository.AddBasketItemAsync(basketItem);
        return Ok(basket);
    }

    [HttpPost("changeBasketItemCount")]
    public async Task<IActionResult> ChangeBasketItemCount([FromBody] BasketItem basketItem, [FromBody]int count)
    {
        var basket = await basketRepository.ChangeBasketItemCount(basketItem, count);
        return Ok(basket);
    }

    [HttpPost("updateBasket")]
    public async Task<IActionResult> UpdateBasketAsync([FromBody]UpdateBasketDto basket)
    {
        var result = await basketRepository.UpdateBasketAsync(basket);
        return Ok(result);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteBasketAsync()
    {
        var result = await basketRepository.DeleteBasketAsync();
        return Ok(result);
    }

}