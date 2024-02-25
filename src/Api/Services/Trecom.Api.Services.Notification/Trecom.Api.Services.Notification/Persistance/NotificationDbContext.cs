using Microsoft.EntityFrameworkCore;
using Trecom.Api.Services.Notification.Models;

namespace Trecom.Api.Services.Notification.Persistance
{
    public class NotificationDbContext: DbContext
    {
        private const string DefaultSchema = "not";

        public NotificationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchema);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Mail> Mails { get; set; }
        public DbSet<Sms> Sms { get; set; }
        public DbSet<NotificationGroup> NotificationGroups { get; set; }
        public DbSet<EmployeeGroup> EmployeeGroups { get; set; }
        public DbSet<CustomerGroup> CustomerGroups { get; set; }
    }
}
