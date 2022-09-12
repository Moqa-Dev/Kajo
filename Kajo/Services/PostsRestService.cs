using Kajo.DataContext;
using Kajo.Models.Dtos;
using Kajo.Models.Entities;
using Kajo.Services.Base;
using System;

namespace Kajo.Services
{
    public class PostsRestService : RestServiceBase<Guid, Post, PostCreateDto, PostUpdateDto>
    {
        public PostsRestService(KajoContext context) : base(context)
        {
        }
    }
}
