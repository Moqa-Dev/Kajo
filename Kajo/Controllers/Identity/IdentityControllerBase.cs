using Kajo.Models.Dtos;
using Kajo.Models.Exceptions;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace Kajo.Controllers.Identity
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(typeof(ErrorDto), 500)]
    [Authorize]
    public class IdentityControllerBase: ODataController
    {

        protected Guid GetCurrentUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        protected void ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Join(System.Environment.NewLine,
                    ModelState.Values
                    .SelectMany(e => e.Errors)
                    .Select(e => e.Exception.Message + e.ErrorMessage));

                errors = "The input was not valid. " + System.Environment.NewLine + errors;

                throw new ApiException(errors, ApiException.INVALID_BODY_CODE);
            }
        }
    }
}
