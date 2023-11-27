using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Trecom.Api.Services.Order.Application.Services.Repositories;
using Trecom.Api.Services.Order.Domain.Entities;
using Trecom.Api.Services.Order.Persistance.Contexts;
using Trecom.Shared.Models;
using Trecom.Shared.Services.Repository;

namespace Trecom.Api.Services.Order.Persistance.Repositories;

public class OrderItemRepository : BaseRepository<OrderItem,OrderDbContext>,IOrderItemRepository
{
    public OrderItemRepository(OrderDbContext dbContext) : base(dbContext)
    {
    }
}