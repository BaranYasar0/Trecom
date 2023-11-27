using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trecom.Shared.Services.Repository.BaseInterfaces;

namespace Trecom.Api.Services.Order.Application.Services.Repositories;

public interface IOrderRepository:IRepository<Domain.Entities.Order>
{
}