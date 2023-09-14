using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trecom.Api.Services.MailService.Services;

namespace Trecom.Api.Services.MailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        private readonly IMailService mailService;

        public MailsController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpGet]
        public async Task<IActionResult> SendEmail()
        {
            await mailService.SendEmailAsync(new[] { "byasarnn@gmail.com","byasarn@gmail.com" }, "deneme",
                "<strong>Bu bir örnek maildir.</strong>", true);

            return Ok();
        }
    }
}
