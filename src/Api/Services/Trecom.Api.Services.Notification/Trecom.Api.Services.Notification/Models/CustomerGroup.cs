using Trecom.Shared.Models;

namespace Trecom.Api.Services.Notification.Models;

public class CustomerGroup : BaseEntity
{
    public Guid CustomerId { get; set; }
    public Guid NotificationGroupId { get; set; }
    public NotificationGroup NotificationGroup { get; set; }
}