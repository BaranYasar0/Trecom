using Trecom.Shared.Models;
using Trecom.Shared.Models.Enums;

namespace Trecom.Api.Services.Notification.Models
{
    public class NotificationGroup:BaseEntity
    {
        public string GroupName { get; set; }
        public string? GroupDescription { get; set; }
        public NotificationGroupType NotificationGroupType { get; set; }
        public List<CustomerGroup> CustomerGroups { get; set; }
        public List<EmployeeGroup> EmployeeGroups { get; set; }
    }
}
