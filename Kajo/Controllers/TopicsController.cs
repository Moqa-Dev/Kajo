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
    [ODataRoutePrefix("Topics")]
    public class TopicsController : ControllerBase<Guid, Topic, TopicCreateDto, TopicUpdateDto>
    {
        public TopicsController(TopicsRestService postsRestService) : base(postsRestService) { }

        [ProducesResponseType(typeof(IEnumerable<Topic>), 200)]
        [Authorize(Permissions.Topics.View)]
        public override async Task<IActionResult> Get() => await base.Get();

        [ODataRoute("{id}")]
        [ProducesResponseType(typeof(Topic), 200)]
        [Authorize(Permissions.Topics.View)]
        public override async Task<IActionResult> GetById([FromODataUri] Guid id) => await base.GetById(id);

        [ProducesResponseType(typeof(Topic), 201)]
        [Authorize(Permissions.Topics.Create)]
        public override async Task<IActionResult> Post([FromBody] TopicCreateDto createDto) => await base.Post(createDto);

        [ODataRoute("{id}")]
        [ProducesResponseType(typeof(Topic), 204)]
        [Authorize(Permissions.Topics.Edit)]
        public override async Task<IActionResult> Put([FromODataUri] Guid id, [FromBody] TopicUpdateDto updateDto)
            => await base.Put(id, updateDto);

        [ODataRoute("{id}")]
        [Authorize(Permissions.Topics.Delete)]
        public override async Task<IActionResult> Delete([FromODataUri] Guid id) => await base.Delete(id);
    }
}
