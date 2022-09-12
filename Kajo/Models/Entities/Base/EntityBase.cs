using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kajo.Models.Entities.Base
{
    public abstract class EntityBase<T> : Auditable
    {
        [Key]
        public T Id { get; set; }

        public override string ToString()
        {
            var modelString = JsonConvert.SerializeObject(this);
            return modelString;
        }
    }
}
