using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Trecom.Api.Services.Order.Domain.Entities;
using Trecom.Api.Services.Order.Persistance.EntitiyConfigurations;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Order.Persistance.Contexts
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<DeliveryCompany> DeliveryCompanies { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Domain.Entities.Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BuyerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DeliveryCompanyEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());
        }

        public override int SaveChanges()
        {
            GenerateDatetime();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            GenerateDatetime();
            return base.SaveChangesAsync(cancellationToken);
        }


        private void GenerateDatetime()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                _ = entry.State switch
                {
                    EntityState.Added => entry.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => entry.Entity.UpdatedDate = DateTime.Now,
                    _ => DateTime.Now
                };
            }
        }
    }
}
