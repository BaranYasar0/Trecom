using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Options;
using Trecom.Api.Services.MailService.Models;

namespace Trecom.Api.Services.MailService.Services;

public class MailService : IMailService
{
    private readonly EmailConfiguration configuration;

    public MailService(IOptions<EmailConfiguration> configuration)
    {
        this.configuration = configuration.Value;
    }

    public async Task SendEmailAsync(string[] tos, string subject, string body, bool isHtmlEnabled = true)
    {
        var smtpClient = SmtpClient();

        await smtpClient.SendMailAsync(
            Message.CreateMailMessage(
                Message.Create(
                    tos,
                    subject,
                    configuration.From,
                    body,
                    isHtmlEnabled),
                configuration)
        );
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