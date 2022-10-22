using Kajo.Models.Dtos;
using Kajo.Models.Exceptions;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Kajo.Configuration.Startup.HostingStartup
{
    public static class ExceptionHandlingHostingStartup
    {

        public static void ExceptionHandlingConfigure(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature?.Error;
                    ApiException apiException =
                        exception is ApiException ex ?
                        ex :
                        new ApiException("Unknown Error!", ApiException.UNKNOWN_ERROR_CODE, exception);

                    var error =  new ErrorDto(apiException);
                    var errorString = error.ToString();

                    context.Response.StatusCode = apiException.StatusCode;
                    context.Response.ContentType = Application.Json;
                    await context.Response.WriteAsync(errorString);

                });
            });
        }
    }
}
