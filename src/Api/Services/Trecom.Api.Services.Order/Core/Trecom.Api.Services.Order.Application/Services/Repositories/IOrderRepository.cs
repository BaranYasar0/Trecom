using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Api.Services.Order.Application.Services.Repositories.BaseInterfaces;

namespace Trecom.Api.Services.Order.Application.Services.Repositories
{
    public interface IOrderRepository:IRepository<Domain.Entities.Order>
    {
    }
}
