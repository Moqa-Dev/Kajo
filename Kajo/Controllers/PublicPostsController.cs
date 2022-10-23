using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Kajo.Models.Dtos;
using Kajo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kajo.Controllers.Base;
using Kajo.Services;
using Kajo.Infrastucture.Identity;
using Microsoft.AspNet.OData.Query;

namespace Kajo.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(ErrorDto), 500)]
    public class PublicPostsController : ODataController
    {
        private readonly PublicPostsService _publicPostsService;
        public PublicPostsController(PublicPostsService publicPostsService){
            _publicPostsService = publicPostsService;
        }

        /// <summary>
        /// Get Dashboard Data.
        /// </summary>
        /// <returns>Dashboard DTO</returns>
        /// <response code="200">IEnumerable<PublicPostDto></response>
        /// <response code="500">Unkown Error</response>
        [HttpGet]
        [ODataRoute("GetPublicPosts")]
        [ProducesResponseType(typeof(IEnumerable<PublicPostDto>), 200)]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Supported)]
        public IActionResult GetPublicPosts() => Ok(_publicPostsService.GetPublicPosts());

        
    }
}
