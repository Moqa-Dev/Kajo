using Kajo.Models.Dtos.Base;
using Kajo.Models.Entities;
using Kajo.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kajo.Models.Dtos
{
    public class PublicPostDto
    {

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public PostStatus Status { get; set; }

        public Guid TopicId { get; set; }

        public String TopicName { get; set; }

        public String UserId { get; set; }

        public String UserName { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public PublicPostDto()
        {
        }

        public override string ToString()
        {
            var modelString = JsonConvert.SerializeObject(this);
            return modelString;
        }
    }
}
