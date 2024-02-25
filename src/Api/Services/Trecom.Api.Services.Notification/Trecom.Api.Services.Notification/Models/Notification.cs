using Trecom.Shared.Models;

namespace Trecom.Api.Services.Notification.Models
{
    public class Notification:BaseEntity
    {
        public string Subject { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string[] Tos { get; set; }
        public DateTime DeliveryTime { get; set; }

        public Notification(string[] tos, string subject, string from, string body)
        {
            Subject = subject;
            From = from;
            Body = body;
            Tos = tos;
        }

        public static Notification Create(string[] tos, string subject, string from, string body, bool isHtmlEnabled = true) => new(tos, subject, from, body);

    }
}
