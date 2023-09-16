namespace Trecom.Api.Services.BasketService.Models;

public record BasketResponseDto(Guid userId, List<BasketItemResponseDto> Items, decimal TotalPrice);

public record BasketItemResponseDto(Guid Id, Guid productId, decimal Price, int Quantity);

public record CreateBasketItemDto(Guid productId, decimal Price, int Quantity);
    
public record UpdateBasketDto(List<CreateBasketItemDto> Items, decimal TotalPrice);