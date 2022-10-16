using log4net;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Kajo.Models.Dtos;
using Kajo.Models.Dtos.Identity;
using Kajo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kajo.DataContext;
using Kajo.Infrastucture.Identity;
using Kajo.Services.Identity;

namespace Kajo.Controllers.Identity
{
    [ODataRoutePrefix("Users")]
    public class UsersController : IdentityControllerBase
    {
        private readonly UsersService _usersService;
        
        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }


        /// <summary>
        /// Query the entity set.
        /// </summary>
        /// <returns>Queried User</returns>
        /// <response code="200">Returns UserDto</response>
        /// <response code="400">In case of Bad request</response>
        /// <response code="500">Unkown Error</response>
        [HttpGet]
        [ODataRoute]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Supported)]
        [Authorize(Permissions.Users.View)]
        public IActionResult Get()
        {
            ValidateModel();
            return Ok(_usersService.Get());
        }


        /// <summary>
        /// Get User by ID.
        /// </summary>
        /// <param id="Guid">The ID of the user</param>
        /// <returns>Quried User</returns>
        /// <response code="200">Returns UserDto</response>
        /// <response code="404">If the item id not found</response>   
        /// <response code="500">Unkown Error</response>
        [HttpGet]
        [ODataRoute("{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Supported)]
        [Authorize(Permissions.Users.View)]
        public async Task<IActionResult> GetById([FromODataUri] String id)
        {
            ValidateModel();
            Guid guid = Guid.Parse(id);
            return Ok(await _usersService.GetById(guid));
        }


        /// <summary>
        /// Deletes a specific Item.
        /// </summary>
        /// <param id="Guid">The ID of the Item to delete</param>
        /// <response code="204">No content for successful delete</response>
        /// <response code="400">If input not valid</response> 
        /// <response code="404">If the item not found</response>  
        /// <response code="500">Unkown Error</response>
        [HttpDelete]
        [ODataRoute("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [Authorize(Permissions.Users.Delete)]
        public async Task<IActionResult> Delete([FromODataUri] String id)
        {
            ValidateModel();
            Guid guid = Guid.Parse(id);
            _usersService.Delete(guid);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}