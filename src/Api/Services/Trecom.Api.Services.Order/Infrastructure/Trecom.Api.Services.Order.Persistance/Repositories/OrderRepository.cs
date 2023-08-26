using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Api.Services.Order.Application.Services.Repositories;
using Trecom.Api.Services.Order.Persistance.Contexts;

namespace Trecom.Api.Services.Order.Persistance.Repositories
{
    public class OrderRepository:BaseRepository<Domain.Entities.Order>,IOrderRepository
    {
        private readonly OrderDbContext dbContext;

        public OrderRepository(OrderDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }



    }
}
