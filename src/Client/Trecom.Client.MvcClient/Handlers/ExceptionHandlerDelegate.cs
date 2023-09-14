using Trecom.Shared.CCS.GlobalException;

namespace Trecom.Client.MvcClient.Handlers
{
    public class ExceptionHandlerDelegate : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var temp = await base.SendAsync(request, cancellationToken);
            var temp2 = temp.Content.Headers.ContentEncoding;
            var type = request.Content.GetType();
            if (type == typeof(BusinessException))
                Console.WriteLine("fkgjdkfşghd");

            return temp;
        }
    }
}
