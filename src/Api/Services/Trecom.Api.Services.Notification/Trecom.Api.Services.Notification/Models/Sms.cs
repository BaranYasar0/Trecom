namespace Trecom.Api.Services.Notification.Models
{
    public class Sms: Notification,INotification
    {
        public Sms(string[] tos, string subject, string from, string body) : base(tos, subject, from, body)
        {
        }
    }
}
