
using Microsoft.EntityFrameworkCore;
using Trecom.Api.Identity.Application.Models.Entities;
using Trecom.Shared.Models;

namespace Trecom.Api.Identity.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        public override int SaveChanges()
        {
            GenerateDateTime();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            GenerateDateTime();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void GenerateDateTime()
        {
            var entities = ChangeTracker.Entries<BaseEntity>();

            foreach (var entity in entities)
            {
                _ = entity.State switch
                {
                    EntityState.Added => entity.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => entity.Entity.UpdatedDate = DateTime.Now,
                    _ => DateTime.Now
                };
            }
        }
    }
}
