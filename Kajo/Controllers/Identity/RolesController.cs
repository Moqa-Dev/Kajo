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
    [ODataRoutePrefix("Roles")]
    public class RolesController : IdentityControllerBase
    {
        private readonly RolesService _rolesService;
 
        public RolesController(RolesService rolesService)
        {
            _rolesService = rolesService;
        }


        /// <summary>
        /// Query the entity set.
        /// </summary>
        /// <returns>Queried Role</returns>
        /// <response code="200">Returns RoleDto</response>
        /// <response code="400">In case of Bad request</response>
        /// <response code="500">Unkown Error</response>
        [HttpGet]
        [ODataRoute]
        [ProducesResponseType(typeof(IEnumerable<RoleDto>), 200)]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Supported)]
        [Authorize(Permissions.Roles.View)]
        public IActionResult Get()
        {
            ValidateModel();
            return Ok(_rolesService.Get());
        }


        /// <summary>
        /// Get by ID.
        /// </summary>
        /// <param id="Guid">The ID of the role</param>
        /// <returns>RoleDto</returns>
        /// <response code="200">Returns RoleDto</response>
        /// <response code="404">If the item id not found</response>   
        /// <response code="500">Unkown Error</response>
        [HttpGet]
        [ODataRoute("{Id}")]
        [ProducesResponseType(typeof(RoleDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Supported)]
        [Authorize(Permissions.Roles.View)]
        public async Task<IActionResult> GetById([FromODataUri] Guid id)
        {
            ValidateModel();
            return Ok(await _rolesService.GetById(id));
        }




        /// <summary>
        /// Creates New item.
        /// </summary>
        /// <param roleDto="UpdateRoleDto">The Item to add</param>
        /// <returns>A newly created item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the Sent item (From Body) issue found</response>
        /// <response code="500">Unkown Error</response>
        [HttpPost]
        [ODataRoute]
        [ProducesResponseType(typeof(RoleDto), 201)]
        [Authorize(Permissions.Roles.Create)]
        public async Task<IActionResult> Post([FromBody] UpdateRoleDto roleDto)
        {
            ValidateModel();
            return Ok(await _rolesService.Post(roleDto));
        }




        /// <summary>
        /// Update All fields for a specific Item.
        /// </summary>
        /// <param id="string">The ID of the Item to update</param>
        /// <param updateRoleDto="UpdateRoleDto">The fields of Item to update</param>
        /// <returns>Updated item</returns>
        /// <response code="204">Item updated successfully</response>
        /// <response code="400">If the Sent item (From Body) issue found</response>
        /// <response code="404">If the item id not found</response>   
        /// <response code="500">Unkown Error</response>
        [HttpPut]
        [ODataRoute("{Id}")]
        [ProducesResponseType(typeof(RoleDto), 204)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [Authorize(Permissions.Roles.Edit)]
        public async Task<IActionResult> Put([FromODataUri] Guid id, [FromBody] UpdateRoleDto update)
        {
            ValidateModel();
            return Updated(await _rolesService.Put(id, update));
        }


        /// <summary>
        /// Deletes a specific Item.
        /// </summary>
        /// <param id="Guid">The ID of the Item to delete</param>
        /// <returns>A newly created Log</returns>
        /// <response code="204">No content for successful delete</response>
        /// <response code="400">If input not valid</response> 
        /// <response code="404">If the item not found</response>  
        /// <response code="500">Unkown Error</response>
        [HttpDelete]
        [ODataRoute("{Id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [Authorize(Permissions.Roles.Delete)]
        public async Task<IActionResult> Delete([FromODataUri] Guid id)
        {
            ValidateModel();
            _rolesService.Delete(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }


    }
}