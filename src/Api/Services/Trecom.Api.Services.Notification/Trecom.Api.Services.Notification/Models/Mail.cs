namespace Trecom.Api.Services.Notification.Models
{
    public class Mail: Notification,INotification
    {
        public bool IsHtmlEnabled { get; set; } = true;

        public Mail(string[] tos, string subject, string from, string body, bool isHtmlEnabled = true) : base(tos, subject, from, body)
        {
            IsHtmlEnabled = isHtmlEnabled;
        }

        public static Mail Create(string[] tos, string subject, string from, string body, bool isHtmlEnabled = true) => new(tos, subject, from, body, isHtmlEnabled);
    }
}
