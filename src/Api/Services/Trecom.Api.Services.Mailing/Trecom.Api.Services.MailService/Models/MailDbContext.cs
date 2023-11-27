using Microsoft.EntityFrameworkCore;

namespace Trecom.Api.Services.MailService.Models;

public class MailDbContext : DbContext
{
    public MailDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("MAILING_SCHEMA");
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Mail> Mails { get; set; }
}


public class Mail
{
    public int Id { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public bool IsHtmlEnabled { get; set; } = true;
    public MailType MailType { get; set; }
}

public enum MailType
{
    NewUser,
    UserConfirmation,
    PasswordReset,
    UserDeleted,
    OrderCreated,
    OrderUpdated,
    OrderCanceled,
    OrderShipped,
    OrderDelivered,
    OrderCompleted
}