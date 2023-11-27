using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Trecom.Api.Services.Order.Application.Services.Repositories;
using Trecom.Api.Services.Order.Domain.Entities;
using Trecom.Api.Services.Order.Persistance.Contexts;
using Trecom.Shared.Services.Repository;

namespace Trecom.Api.Services.Order.Persistance.Repositories;

public class BuyerRepository : BaseRepository<Buyer,OrderDbContext>,IBuyerRepository
{
    public BuyerRepository(OrderDbContext dbContext) : base(dbContext)
    {
    }
}