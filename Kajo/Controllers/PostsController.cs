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

namespace Kajo.Controllers
{
    [ODataRoutePrefix("Posts")]
    public class PostsController : ControllerBase<Guid, Post, PostCreateDto, PostUpdateDto>
    {
        public PostsController(PostsRestService postsRestService) : base(postsRestService){}

        [ProducesResponseType(typeof(IEnumerable<Post>), 200)]
        [Authorize(Permissions.Posts.View)]
        public override async Task<IActionResult> Get() => await base.Get();

        [ODataRoute("{id}")]
        [ProducesResponseType(typeof(Post), 200)]
        [Authorize(Permissions.Posts.View)]
        public override async Task<IActionResult> GetById([FromODataUri] Guid id) => await base.GetById(id);
        
        [ProducesResponseType(typeof(Post), 201)]
        [Authorize(Permissions.Posts.Create)]
        public override async Task<IActionResult> Post([FromBody] PostCreateDto createDto) => await base.Post(createDto);

        [ODataRoute("{id}")]
        [ProducesResponseType(typeof(Post), 204)]
        [Authorize(Permissions.Posts.Edit)]
        public override async Task<IActionResult> Put([FromODataUri] Guid id, [FromBody] PostUpdateDto updateDto)
            => await base.Put(id, updateDto);

        [ODataRoute("{id}")]
        [Authorize(Permissions.Posts.Delete)]
        public override async Task<IActionResult> Delete([FromODataUri] Guid id) => await base.Delete(id);
    }
}
