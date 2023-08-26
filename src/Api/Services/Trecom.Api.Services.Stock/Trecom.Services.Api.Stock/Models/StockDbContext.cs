using Microsoft.EntityFrameworkCore;

namespace Trecom.Services.Api.Stock.Models
{
    public class StockDbContext:DbContext
    {
        public StockDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }
    }
}
