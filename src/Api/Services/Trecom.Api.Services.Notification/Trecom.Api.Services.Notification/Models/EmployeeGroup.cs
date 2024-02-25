using Trecom.Shared.Models;

namespace Trecom.Api.Services.Notification.Models;

public class EmployeeGroup : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Guid NotificationGroupId { get; set; }
    public NotificationGroup NotificationGroup { get; set; }
}