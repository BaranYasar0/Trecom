using Trecom.Api.Services.Order.Application.Services.Repositories;
using Trecom.Api.Services.Order.Domain.Entities;
using Trecom.Api.Services.Order.Persistance.Contexts;

namespace Trecom.Api.Services.Order.Persistance.Repositories;

public class OrderItemRepository : BaseRepository<OrderItem>,IOrderItemRepository
{
    public OrderItemRepository(OrderDbContext dbContext) : base(dbContext)
    {
    }
}