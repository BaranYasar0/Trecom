using MassTransit;
using Trecom.Api.Identity.Application.Events;
using Trecom.Api.Services.MailService.Constants;
using Trecom.Api.Services.MailService.Services;

namespace Trecom.Api.Services.MailService.Consumers;

public class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
{
    private readonly IMailService mailService;

    public UserCreatedEventConsumer(IMailService mailService)
    {
        this.mailService = mailService;
    }

    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        try
        {
            await mailService.SendEmailAsync("byasarn@gmail.com", MailingConstants.UserCreatedMailSubject,
                @"<span class=""badge badge-pill badge-light"">asda</span>");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}