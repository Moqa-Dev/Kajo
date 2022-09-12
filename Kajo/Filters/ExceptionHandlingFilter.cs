using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OData;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System;
using System.Drawing;
using System.Linq;
using Kajo.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Kajo.Models.Dtos;

namespace Kajo.Filters
{
    public class ExceptionHandlingFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            HandleAppExceptions(context);
            HandleODataExceptions(context);
        }

        public void HandleODataExceptions(ActionExecutedContext context)
        {
            if (context?.Result is BadRequestObjectResult ob &&
                ob.Value is SerializableError se &&
                se.Keys.Contains("ExceptionType") &&
                se["ExceptionType"].ToString().Equals("Microsoft.OData.ODataException"))
            {
                var exception = new ApiException("OData Error: " + se["Message"], ApiException.ODATA_ERROR);
                context.Result = new ContentResult()
                {
                    Content = new ErrorDto(exception).ToString(),
                    StatusCode = exception.StatusCode,
                    ContentType = "application/json",
                };
            }
        }

        public void HandleAppExceptions(ActionExecutedContext context)
        {
            //Do Nothing as App Exceptions are handled by error Controller
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
