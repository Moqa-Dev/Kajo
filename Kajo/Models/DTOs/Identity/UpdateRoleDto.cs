using Newtonsoft.Json;

namespace Kajo.Models.Dtos.Identity
{
    public class UpdateRoleDto
    {
        public string Role { get; set; }

        public override string ToString()
        {
            var modelString = JsonConvert.SerializeObject(this);
            return modelString;
        }
    }
}
