using Kajo.Models.Dtos.Base;
using Kajo.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kajo.Models.Dtos
{
    public class TopicCreateDto : ICreateDto<Topic>
    {

        public const int MaxTitleLength = 128;
        public const int MinDescriptionLength = 10;

        [Required]
        [StringLength(MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        [MinLength(MinDescriptionLength)]
        public string Description { get; set; }

        public Topic ToEntity(String userID)
        {
            return new Topic()
            {
                Title = this.Title,
                Description = this.Description,
                UserID = userID,
            };
        }

        public override string ToString()
        {
            var modelString = JsonConvert.SerializeObject(this);
            return modelString;
        }
    }
}
