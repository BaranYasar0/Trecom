using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Api.Services.Order.Application.Services.Repositories;
using Trecom.Api.Services.Order.Domain.Entities;
using Trecom.Api.Services.Order.Persistance.Contexts;

namespace Trecom.Api.Services.Order.Persistance.Repositories;

public class OrderDetailRepository:BaseRepository<OrderDetail>,IOrderDetailRepository
{
    public OrderDetailRepository(OrderDbContext dbContext) : base(dbContext)
    {
    }
}