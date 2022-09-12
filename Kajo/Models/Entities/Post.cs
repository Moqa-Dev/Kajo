using Kajo.Models.Entities.Base;
using Kajo.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kajo.Models.Entities
{
    public class Post : UserSpecificAuditable<Guid>
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
        public Guid TopicId { get; set; }

        public virtual Topic Topic { get; set; }

    }
}
