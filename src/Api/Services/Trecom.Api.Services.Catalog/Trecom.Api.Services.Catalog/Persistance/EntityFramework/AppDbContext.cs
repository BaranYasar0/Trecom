using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Trecom.Api.Services.Catalog.Models.Entities;
using Trecom.Api.Services.Catalog.Models.Enums;
using Trecom.Api.Services.Catalog.Persistance.DataSeeding;
using Trecom.Api.Services.Catalog.Persistance.EntityFramework.EntityConfigurations;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Catalog.Persistance.EntityFramework;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<BaseCategory> BaseCategories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<SpecificCategory> SpecificCategories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<TypeCategory> TypeCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BaseCategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BrandEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SpecificCategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SupplierEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TypeCategoryEntityConfiguration());

        SeedCatalogItems.SeedItemsToDb(modelBuilder);
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
                EntityState.Added => entry.Entity.CreatedDate = DateTime.Now,
                EntityState.Modified => entry.Entity.UpdatedDate = DateTime.Now,
                _ => DateTime.Now
            };
        }
    }
}