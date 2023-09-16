using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Trecom.Shared.Pipelines.Logging;

public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>,ILoggingBehaviour
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger;
    private readonly IHttpContextAccessor httpContextAccessor;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger, IHttpContextAccessor httpContextAccessor)
    {
        this.logger = logger;
        this.httpContextAccessor = httpContextAccessor;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation($"{request.GetType().Name} started to handling!");

        LogParameters logParameters = new()
        {
            UserName = httpContextAccessor.HttpContext == null ||
                       httpContextAccessor.HttpContext.User.Identity.Name == null
                ? "Unknown"
                : httpContextAccessor.HttpContext.User.Identity.Name,
            MethodName = next.Method.Name,
            Type = request.GetType().Name
        };

        logger.LogInformation($"{logParameters.UserName},{logParameters.MethodName},{logParameters.Type}\n{logParameters.Type} handled!");

        return next();
    }
}