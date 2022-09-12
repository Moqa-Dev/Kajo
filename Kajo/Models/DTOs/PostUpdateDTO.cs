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
    public class PostUpdateDto : IUpdateDto<Post>
    {
        public const int MaxTitleLength = 128;
        public const int MinContentLength = 10;

        [Required]
        [StringLength(MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        [MinLength(MinContentLength)]
        public string Content { get; set; }

        [Required]
        [EnumDataType(typeof(PostStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        public PostStatus Status { get; set; }

        [Required]
        public Guid TopicID { get; set; }

        public void Update(Post entity)
        {
            entity.Title = this.Title;
            entity.Content = this.Content;
            entity.Status = this.Status;
            entity.TopicId = this.TopicID;
        }

        public override string ToString()
        {
            var modelString = JsonConvert.SerializeObject(this);
            return modelString;
        }
    }
}
