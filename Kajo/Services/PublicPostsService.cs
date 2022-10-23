using Kajo.DataContext;
using Kajo.Models.Dtos;
using Kajo.Models.Entities;
using Kajo.Models.Enums;
using Kajo.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using static Kajo.Infrastucture.Identity.Permissions;

namespace Kajo.Services
{
    public class PublicPostsService
    {
        private readonly KajoContext _context;
        public PublicPostsService(KajoContext context)
        {
            _context = context;
        }

        public List<PublicPostDto> GetPublicPosts()
        {
            return _context.Posts
                .Where(p=> p.Status.Equals(PostStatus.Public))
            .Select(post=> new PublicPostDto()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Status = post.Status,
                TopicId = post.TopicId,
                TopicName = post.Topic.Title,
                UserId = post.UserID,
                UserName = post.User.UserName,
                CreationDate = post.CreationDate,
                UpdateDate = post.UpdateDate,
            })
                .ToList();
        }
    }
}
