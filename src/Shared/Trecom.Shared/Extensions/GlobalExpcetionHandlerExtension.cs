using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Trecom.Shared.CCS.GlobalException;

namespace Trecom.Shared.Extensions
{
    public static class GlobalExpcetionHandlerExtension
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandler>();
        }
    }
}
