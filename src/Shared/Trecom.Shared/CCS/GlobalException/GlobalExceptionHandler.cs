using System.Net;
using System.Net.Http;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Trecom.Shared.Models;

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
            var errors = ((FluentValidation.ValidationException)exception).Errors;
            

            return context.Response.WriteAsync(new ApiResponse()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Title = "Validation error(s)",
                Errors = errors.Select(x=>x.ErrorMessage).ToList(),
                ResponseTime = DateTime.Now,
                IsSuccess = false
            }.ToString());
        }

        private Task AuthorizationException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);

            return context.Response.WriteAsync(new ApiResponse()
            {
                Title = "Authorization Exception",
                Errors = new List<string> { exception.Message },
                //RequestName = _next.Method.Name,
                StatusCode = (int)HttpStatusCode.Unauthorized,
                ResponseTime = DateTime.Now,
                IsSuccess = false
            }.ToString());
        }

        private Task GeneralException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ApiResponse()
            {
                Title = "Internal Exception",
                Errors = new List<string> { exception.Message },
                StatusCode = StatusCodes.Status400BadRequest,
                ResponseTime = DateTime.Now,
                IsSuccess = false
            }.ToString());
        }

        private Task BusinessException(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

            return httpContext.Response.WriteAsync(new ApiResponse()
            {
                Title = "Business Exception",
                Errors = new List<string> { exception.Message },
                StatusCode = StatusCodes.Status400BadRequest,
                ResponseTime = DateTime.Now,
                IsSuccess = false
            }.ToString());
        }
    }


}
