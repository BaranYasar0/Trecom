namespace Trecom.Client.MvcClient.Middlewares
{
    public class ClientExceptionHandler
    {
        private readonly RequestDelegate next;

        public ClientExceptionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        private ValueTask HandleException(HttpContext context,Exception e)
        {
            context.Response.Redirect("/Home/Error");
            return ValueTask.CompletedTask;
        }
    }
}
