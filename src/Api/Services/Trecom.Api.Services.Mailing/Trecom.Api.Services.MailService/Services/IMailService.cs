namespace Trecom.Api.Services.MailService.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string[] tos, string subject, string body, bool isHtmlEnabled = true);
        Task SendEmailAsync(string to, string subject, string body, bool isHtmlEnabled=true);
    }
}
