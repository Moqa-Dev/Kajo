using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kajo.Models.Dtos.Identity
{
    public class RoleDto
    {
        [Key]
        public string RoleID { get; set; }

        public string Role { get; set; }

        public virtual List<string> Permissions { get; set; }

        public virtual List<UserDto> Users { get; set; }


        public override string ToString()
        {
            var modelString = JsonConvert.SerializeObject(this);
            return modelString;
        }
    }
}
