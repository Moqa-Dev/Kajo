using log4net;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Kajo.Models.Dtos;
using Kajo.Models.Dtos.Identity;
using Kajo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kajo.DataContext;
using Kajo.Infrastucture.Identity;
using Kajo.Services.Identity;

namespace Kajo.Controllers.Identity
{
    public class ProfileServicesController : IdentityControllerBase
    {
        private readonly UsersService _usersService;
        
        public ProfileServicesController(UsersService usersService)
        {
            _usersService = usersService;
        }


        /// <summary>
        /// UpdatePassword.
        /// </summary>
        /// <param model="UpdatePasswordDto">UpdatePasswordDto</param>
        /// <returns>Updated item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the Sent item (From Body) issue found</response>
        /// <response code="500">Unkown Error</response>
        [HttpPost]
        [ODataRoute("UpdatePassword")]
        [ProducesResponseType(typeof(TokenDto), 204)]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto model)
        {
            ValidateModel();
            return Updated(await _usersService.UpdatePassword(GetCurrentUserId(), model));
        }

        /// <summary>
        /// GetUserData
        /// </summary>
        /// <returns>UserDto</returns>
        /// <response code="200">Returns UserDto</response>
        /// <response code="404">If the item id not found</response>   
        /// <response code="500">Unkown Error</response>
        [HttpGet]
        [ODataRoute("GetUserData")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Supported)]
        public async Task<IActionResult> GetUserData()
        {
            ValidateModel();
            return Ok(await _usersService.GetUserData(GetCurrentUserId()));
        }

        /// <summary>
        /// UpdateUserProfile.
        /// </summary>
        /// <param updateUserDto="UpdateUserDto">UpdateUserDto</param>
        /// <returns>UserDto</returns>
        /// <response code="204">Item updated successfully</response>
        /// <response code="400">If the Sent item (From Body) issue found</response>
        /// <response code="404">If the item id not found</response>   
        /// <response code="500">Unkown Error</response>
        [HttpPost]
        [ODataRoute("UpdateUserProfile")]
        [ProducesResponseType(typeof(UserDto), 204)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [Authorize(Permissions.Profile.Edit)]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserDto updateUserDto)
        {
            ValidateModel();
            return Updated( await _usersService.UpdateUserData(GetCurrentUserId(), updateUserDto));
        }

    }
}
