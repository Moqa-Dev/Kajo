using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Kajo.Models.Dtos;
using Kajo.Models.Dtos.Identity;
using System.Threading.Tasks;
using Kajo.Infrastucture.Identity;
using Kajo.Services.Identity;

namespace Kajo.Controllers.Identity
{
    public class AuthenticateServicesController : IdentityControllerBase
    {
        private readonly AuthenticateService _authenticateService;
        
        public AuthenticateServicesController(AuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <remarks>
        /// Sample request
        /// </remarks>
        /// <param model="RegisterUserDto">RegisterUserDto</param>
        /// <returns>A newly created item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the Sent item (From Body) issue found</response>
        /// <response code="500">Unkown Error</response>
        [HttpPost]
        [ODataRoute("Register")]
        [ProducesResponseType(typeof(RegisterUserDto), 201)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [Authorize(Permissions.Users.Create)]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
        {
            ValidateModel();
            return Created(await _authenticateService.Register(model));
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param model="LoginUserDto">LoginUserDto</param>
        /// <returns>TokenDto</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the Sent item (From Body) issue found</response>
        /// <response code="500">Unkown Error</response>
        [HttpPost]
        [ODataRoute("Login")]
        [ProducesResponseType(typeof(TokenDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserDto model)
        {
            ValidateModel();
            return Created(await _authenticateService.Login(model));
        }
    }
}
