using Kajo.Models.Dtos;
using Kajo.Models.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Kajo.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ErrorDto Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            ApiException apiException =
                exception is ApiException ex ?
                ex :
                new ApiException("Unknown Error!", ApiException.UNKNOWN_ERROR_CODE, exception);

            Response.StatusCode = apiException.StatusCode;
            return new ErrorDto(apiException);
        }
    }
}
