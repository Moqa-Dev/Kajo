using Kajo.DataContext;
using Kajo.Models.Dtos;
using Kajo.Models.Entities;
using Kajo.Services.Base;
using System;

namespace Kajo.Services
{
    public class TopicsRestService : RestServiceBase<Guid, Topic, TopicCreateDto, TopicUpdateDto>
    {
        public TopicsRestService(KajoContext context) : base(context)
        {
        }
    }
}
