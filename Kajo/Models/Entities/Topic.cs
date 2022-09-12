using Kajo.Models.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kajo.Models.Entities
{
    public class Topic : UserSpecificAuditable<Guid>
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

        public virtual ICollection<Post> Posts { get; protected set; }

    }
}
