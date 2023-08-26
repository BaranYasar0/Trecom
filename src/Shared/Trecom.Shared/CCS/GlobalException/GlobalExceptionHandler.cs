using System.Net;
using System.Net.Http;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Trecom.Shared.CCS.GlobalException
{
    public class GlobalExceptionHandler
    {
        private RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleException(context, exception);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception.GetType() == typeof(AuthorizationException))
                return AuthorizationException(context, exception);

            else if (exception.GetType() == typeof(BusinessException))
                return BusinessException(context, exception);

            else if (exception.GetType() == typeof(ValidationException))
                return ValidationException(context, exception);

            else
                return GeneralException(context, exception);

        }

        private Task ValidationException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
            object errors = ((FluentValidation.ValidationException)exception).Errors;

            return context.Response.WriteAsync(new ValidationExceptionDetails()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Title = "Validation error(s)",
                Detail = "",
                RequestName = _next.Method.Name,
                Errors = errors
            }.ToString());
        }

        private Task AuthorizationException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);

            return context.Response.WriteAsync(new AuthorizationExceptionDetails()
            {
                Title = "Authorization Exception",
                Detail = exception.Message,
                RequestName = _next.Method.Name,
                StatusCode = (int)HttpStatusCode.Unauthorized,
                ThrownDate = DateTime.Now
            }.ToString());
        }

        private Task GeneralException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new GeneralExceptionDetails()
            {
                Title = "Internal Exception",
                Detail = exception.Message,
                StatusCode = StatusCodes.Status400BadRequest,
                ThrownDate = DateTime.Now
            }.ToString());
        }

        private Task BusinessException(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

            return httpContext.Response.WriteAsync(new BusinessExceptionDetails()
            {
                Title = "Business Exception",
                Detail = exception.Message,
                RequestName = _next.Method.Name,
                StatusCode = StatusCodes.Status400BadRequest,
                ThrownDate = DateTime.Now
            }.ToString());
        }
    }


}
