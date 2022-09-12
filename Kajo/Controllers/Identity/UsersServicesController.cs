using log4net;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Kajo.Models.Dtos;
using Kajo.Models.Dtos.Identity;
using Kajo.Models.Entities;
using System;
using System.Threading.Tasks;
using Kajo.DataContext;
using Kajo.Infrastucture.Identity;
using Kajo.Services.Identity;

namespace Kajo.Controllers.Identity
{
    public class UsersServicesController : IdentityControllerBase
    {
        private readonly UsersService _usersService;
        
        public UsersServicesController(UsersService usersService)
        {
            _usersService = usersService;
        }



        /// <summary>
        /// Update All fields for a specific Item.
        /// </summary>
        /// <param userId="Guid">The ID of the Item to update</param>
        /// <param updateUserDto="UpdateUserDto">The fields of Item to update</param>
        /// <returns>Updated item</returns>
        /// <response code="201">Item updated successfully</response>
        /// <response code="400">If the Sent item (From Body) issue found</response>
        /// <response code="404">If the item id not found</response>   
        /// <response code="500">Unkown Error</response>
        [HttpPost]
        [ODataRoute("EditUserData(userId={userId})")]
        [ProducesResponseType(typeof(UserDto), 201)]
        [Authorize(Permissions.Users.Create)]
        [Authorize(Permissions.UsersService.EditUserData)]
        [AllowAnonymous]
        public async Task<IActionResult> EditUserData([FromODataUri] Guid userId, [FromBody] UpdateUserDto updateUserDto)
        {
            ValidateModel();
            return Updated(await _usersService.UpdateUserData(userId, updateUserDto));
        }


        /// <summary>
        /// Login using Username, Password.
        /// </summary>
        /// <param userId="Guid">The ID of the Item to update</param>
        /// <param model="ResetPasswordDto">ResetPasswordDto</param>
        /// <returns>Updated item</returns>
        /// <response code="201">Item updated successfully</response>
        /// <response code="400">If the Sent item (From Body) issue found</response>
        /// <response code="500">Unkown Error</response>
        [HttpPost]
        [ODataRoute("UpdateUserPassword(userId={userId})")]
        [ProducesResponseType(typeof(UserDto), 201)]
        [Authorize(Permissions.UsersService.UpdateUserPassword)]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUserPassword([FromODataUri] Guid userId, [FromBody] ResetPasswordDto model)
        {
            ValidateModel();
            return Updated(await _usersService.ResetPassword(userId, model));
        }

    }
}
