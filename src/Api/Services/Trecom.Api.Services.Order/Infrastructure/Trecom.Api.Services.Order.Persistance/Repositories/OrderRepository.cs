using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Api.Services.Order.Application.Services.Repositories;
using Trecom.Api.Services.Order.Persistance.Contexts;
using Trecom.Shared.Services.Repository;

namespace Trecom.Api.Services.Order.Persistance.Repositories;

public class OrderRepository:BaseRepository<Domain.Entities.Order,OrderDbContext>,IOrderRepository
{
    public OrderRepository(OrderDbContext dbContext) : base(dbContext)
    {
    }
}