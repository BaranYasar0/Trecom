using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using Trecom.Api.Services.Notification.Models;
using Trecom.Shared.Models;

namespace Trecom.Api.Services.Notification.Services
{
    public class MailService
    {
        private readonly EmailConfiguration configuration;

        public MailService(IOptions<EmailConfiguration> configuration)
        {
            this.configuration = configuration.Value;
        }

        public async Task SendEmailAsync(string[] tos, string subject, string body, bool isHtmlEnabled = true)
        {
            var smtpClient = SmtpClient();
            Mail mail = Mail.Create(tos,subject,configuration.From,body);
            await smtpClient.SendMailAsync(Mail.CreateMailMessage(mail,configuration));
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isHtmlEnabled)
        {
            await SendEmailAsync(new string[] { to }, subject, body, isHtmlEnabled);
        }

        private SmtpClient SmtpClient()
        {
            SmtpClient smtpClient = new();
            smtpClient.Credentials = new NetworkCredential(configuration.Username, configuration.Password);
            smtpClient.Port = int.Parse(configuration.Port);
            smtpClient.Host = configuration.SmtpServer;
            smtpClient.EnableSsl = true;
            return smtpClient;
        }
    }
}
