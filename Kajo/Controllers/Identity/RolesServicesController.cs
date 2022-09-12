using log4net;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Kajo.Models.Dtos;
using Kajo.Models.Dtos.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kajo.Infrastucture.Identity;
using Kajo.Services.Identity;

namespace Kajo.Controllers.Identity
{
    public class RolesServicesController : IdentityControllerBase
    {
        private readonly RolesService _rolesService;
        
        public RolesServicesController(RolesService rolesService)
        {
            _rolesService = rolesService;
        }


        /// <summary>
        /// Get User by ID.
        /// </summary>
        /// <param roleId="Guid">roleId</param>
        /// <param permissions="JArray">permissions</param>
        /// <returns>RoleDto</returns>
        /// <response code="200">Returns UserDto</response>
        /// <response code="404">If the item id not found</response>   
        /// <response code="500">Unkown Error</response>
        [HttpPost]
        [ODataRoute("UpdateRolePermissions(roleId={roleId})")]
        [ProducesResponseType(typeof(RoleDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.None)]
        [Authorize(Permissions.RolesService.UpdateRolePermissions)]
        public async Task<IActionResult> UpdateRolePermissions([FromODataUri] Guid roleId, [FromBody] JArray permissions)
        {
            ValidateModel();
            return Updated(await _rolesService.UpdateRolePermissions(roleId, permissions));
        }


        /// <summary>
        /// Get User by ID.
        /// </summary>
        /// <param roleId="Guid">The ID of the Item to update</param>
        /// <param usersIds="JArray">The fields of Item to update</param>
        /// <returns>RoleDto</returns>
        /// <response code="200">Returns UserDto</response>
        /// <response code="404">If the item id not found</response>   
        /// <response code="500">Unkown Error</response>
        [HttpPost]
        [ODataRoute("UpdateRoleUsers(roleId={roleId})")]
        [ProducesResponseType(typeof(RoleDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.None)]
        [Authorize(Permissions.RolesService.UpdateRoleUsers)]
        public async Task<IActionResult> UpdateRoleUsers([FromODataUri] Guid roleId, [FromBody] JArray usersIds)
        {
            ValidateModel();
            return Updated(await _rolesService.UpdateRoleUsers(roleId, usersIds));
        }

        /// <summary>
        /// Get User by ID.
        /// </summary>
        /// <param userId="Guid">The ID of the Item to update</param>
        /// <param rolesIds="JArray">The fields of Item to update</param>
        /// <returns>UserDto</returns>
        /// <response code="200">Returns UserDto</response>
        /// <response code="404">If the item id not found</response>   
        /// <response code="500">Unkown Error</response>
        [HttpPost]
        [ODataRoute("UpdateUserRoles(userId={userId})")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.None)]
        [Authorize(Permissions.RolesService.UpdateUserRoles)]
        public async Task<IActionResult> UpdateUserRoles([FromODataUri] Guid userId, [FromBody] JArray rolesIds)
        {
            ValidateModel();
            return Updated(await _rolesService.UpdateUserRoles(userId, rolesIds));
        }

        /// <summary>
        /// GetUserPermissions
        /// </summary>
        /// <returns>Permissions</returns>
        /// <response code="200">Returns UserDto</response>
        /// <response code="404">If the item id not found</response>   
        /// <response code="500">Unkown Error</response>
        [HttpGet]
        [ODataRoute("GetUserPermissions")]
        [ProducesResponseType(typeof(List<string>), 200)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.None)]
        public async Task<IActionResult> GetUserPermissions()
        {
            ValidateModel();
            return Ok(await _rolesService.GetUserPermissions(GetCurrentUserId()));
        }


        /// <summary>
        /// GetAllPermissions
        /// </summary>
        /// <returns>Permissions</returns>
        /// <response code="200">Returns UserDto</response>
        /// <response code="404">If the item id not found</response>   
        /// <response code="500">Unkown Error</response>
        [HttpGet]
        [ODataRoute("GetAllPermissions")]
        [ProducesResponseType(typeof(List<string>), 200)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.None)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPermissions()
        {
            ValidateModel();
            return Ok(await _rolesService.GetAllPermissions());
        }

    }
}
