using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kajo.Models.Entities.Base
{
    public abstract class UserSpecificAuditable<T> : EntityBase<T>
    {
        [Required]
        public String UserID { get; set; }

        public virtual KajoUser User { get; set; }

        public override string ToString()
        {
            var modelString = JsonConvert.SerializeObject(this);
            return modelString;
        }
    }
}
