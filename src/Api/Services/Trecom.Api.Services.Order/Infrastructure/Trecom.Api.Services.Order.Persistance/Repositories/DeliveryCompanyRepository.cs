using Trecom.Api.Services.Order.Application.Services.Repositories;
using Trecom.Api.Services.Order.Domain.Entities;
using Trecom.Api.Services.Order.Persistance.Contexts;
using Trecom.Shared.Services.Repository;

namespace Trecom.Api.Services.Order.Persistance.Repositories;

public class DeliveryCompanyRepository : BaseRepository<DeliveryCompany,OrderDbContext>,IDeliveryCompanyRepository
{
    public DeliveryCompanyRepository(OrderDbContext dbContext) : base(dbContext)
    {
    }
}