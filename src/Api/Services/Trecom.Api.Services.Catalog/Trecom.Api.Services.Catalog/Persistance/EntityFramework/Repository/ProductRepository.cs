using Trecom.Api.Services.Catalog.Models.Entities;
using Trecom.Api.Services.Catalog.Persistance.EntityFramework.Repository.Interfaces;
using Trecom.Shared.Services.Repository;

namespace Trecom.Api.Services.Catalog.Persistance.EntityFramework.Repository
{
    public class ProductRepository:BaseRepository<Product,AppDbContext>,IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
