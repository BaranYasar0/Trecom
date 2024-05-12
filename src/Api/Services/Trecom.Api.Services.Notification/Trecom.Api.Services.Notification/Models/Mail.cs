using System.Net.Mail;
using System.Text;
using Trecom.Shared.Models;

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

        public static MailMessage CreateMailMessage(Mail mail, EmailConfiguration configuration)
        {
            MailMessage mailMessage = new();
            mailMessage.IsBodyHtml = mail.IsHtmlEnabled;
            mail.Tos.ToList().ForEach(x => mailMessage.To.Add(x));
            mailMessage.Subject = mail.Subject;
            mailMessage.Body = mail.Body;
            mailMessage.From = new(configuration.From, configuration.Username, Encoding.UTF8);

            return mailMessage;
        }
    }
}
