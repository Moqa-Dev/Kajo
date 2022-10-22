using Kajo.Models.Dtos;
using Kajo.Models.Dtos.Base;
using Kajo.Models.Entities;
using Kajo.Models.Entities.Base;
using Kajo.Models.Exceptions;
using Kajo.Services.Base;
using log4net;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace Kajo.Controllers.Base
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(typeof(ErrorDto), 500)]
    [Authorize]
    public abstract class ControllerBase<TId, TEntity, TCreateDto, TUpdateDto> : ODataController
        where TEntity : EntityBase<TId>
        where TCreateDto : ICreateDto<TEntity>
        where TUpdateDto : IUpdateDto<TEntity>
    {
        private readonly IRestService<TId, TEntity, TCreateDto, TUpdateDto> _restService;
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ControllerBase(IRestService<TId, TEntity, TCreateDto, TUpdateDto> restService)
        {
            _restService = restService;
        }

        /// <summary>
        /// Query the entity set.
        /// </summary>
        /// <returns>Queried Items</returns>
        /// <response code="200">Returns items</response>
        /// <response code="400">In case of Bad request</response>
        /// <response code="500">Unkown Error</response>
        [HttpGet]
        [ODataRoute]
        //[ProducesResponseType(typeof(IEnumerable<TEntity>), 200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Supported)]
        //[Authorize(Permissions.Posts.View)]
        public virtual async Task<IActionResult> Get()
        {
            ValidateModel();
            return Ok(await _restService.GetAll());
        }


        /// <summary>
        /// Get item by ID.
        /// </summary>
        /// <param Id="TId">The Item ID</param>
        /// <returns>item with ID specified in URL</returns>
        /// <response code="200">Returns item</response>
        /// <response code="404">If the item id not found</response>  
        /// <response code="500">Unkown Error</response>
        [HttpGet]
        [ODataRoute("{id}")]
        //[ProducesResponseType(typeof(TEntity), 200)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Supported)]
        //[Authorize(Permissions.Posts.View)]
        public virtual async Task<IActionResult> GetById([FromODataUri] TId id)
        {
            ValidateModel();
            var entity = await _restService.GetById(id);
            return Ok(entity);
        }

        /// <summary>
        /// Creates New item.
        /// </summary>
        /// <param createDto="TCreateDto">The Item to add</param>
        /// <returns>A newly created item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the Sent item (From Body) issue found</response>
        /// <response code="500">Unkown Error</response>
        [HttpPost]
        [ODataRoute]
        //[ProducesResponseType(typeof(TEntity), 201)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        //[Authorize(Permissions.Posts.Create)]
        public virtual async Task<IActionResult> Post([FromBody] TCreateDto createDto)
        {
            ValidateModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            TEntity entity = await _restService.Create(userId, createDto);
            return Created(entity);
        }

        /// <summary>
        /// Update All fields for a specific Item.
        /// </summary>
        /// <param id="Guid">The ID of the Item to update</param>
        /// <param updateDto="TUpdateDto">The fields of Item to update</param>
        /// <returns>Updated item</returns>
        /// <response code="204">Item updated successfully</response>
        /// <response code="400">If the Sent item (From Body) issue found</response>
        /// <response code="404">If the item id not found</response>   
        /// <response code="500">Unkown Error</response>
        [HttpPut]
        [ODataRoute("{id}")]
        //[ProducesResponseType(typeof(TEntity), 204)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        //[Authorize(Permissions.Posts.Edit)]
        public virtual async Task<IActionResult> Put([FromODataUri] TId id, [FromBody] TUpdateDto updateDto)
        {
            ValidateModel();
            TEntity entity = await _restService.Update(id, updateDto);
            return Updated(entity);
        }



        /// <summary>
        /// Deletes a specific Item.
        /// </summary>
        /// /// <param id="TId">The ID of the Item to delete</param>
        /// <returns>A newly created item</returns>
        /// <response code="204">No content for successful delete</response>
        /// <response code="404">If the item not found</response>  
        /// <response code="500">Unkown Error</response>
        [HttpDelete]
        [ODataRoute("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDto), 404)]
        //[Authorize(Permissions.Posts.Delete)]
        public virtual async Task<IActionResult> Delete([FromODataUri] TId id)
        {
            ValidateModel();
            await Task.Run(() => _restService.Delete(id));
            return StatusCode(StatusCodes.Status204NoContent);
        }

        protected void ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Join(System.Environment.NewLine,
                    ModelState.Values
                    .SelectMany(e => e.Errors)
                    .Select(e => e.ErrorMessage));

                errors = "The input was not valid. " + System.Environment.NewLine + errors;

                throw new ApiException(errors, ApiException.INVALID_BODY_CODE);
            }
        }

    }
}
