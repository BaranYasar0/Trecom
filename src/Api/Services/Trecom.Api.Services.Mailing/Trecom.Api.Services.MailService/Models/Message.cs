using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Text;

namespace Trecom.Api.Services.MailService.Models
{
    public class Message
    {
        public string Subject { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string[] Tos { get; set; }
        public bool isHtmlEnabled { get; set; } = true;

        public Message(string[] tos, string subject, string from, string body, bool isHtmlEnabled = true)
        {
            Subject = subject;
            From = from;
            Body = body;
            Tos = tos;
            this.isHtmlEnabled = isHtmlEnabled;
        }

        public static Message Create(string[] tos, string subject, string from, string body, bool isHtmlEnabled = true) => new(tos, subject, from, body,isHtmlEnabled);


        public static MailMessage CreateMailMessage(Message message, EmailConfiguration configuration)
        {
            MailMessage mailMessage = new();
            mailMessage.IsBodyHtml = message.isHtmlEnabled;
            message.Tos.ToList().ForEach(x => mailMessage.To.Add(x));

            mailMessage.Subject = message.Subject;
            mailMessage.Body = message.Body;
            mailMessage.From = new(configuration.From, configuration.Username, Encoding.UTF8);

            return mailMessage;
        }

    }
}
