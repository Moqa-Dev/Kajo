using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kajo.Models.Dtos.Identity
{
    public class UpdatePasswordDto
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        public override string ToString()
        {
            var model = new UpdatePasswordDto()
            {
                OldPassword = "Encrypted",
                NewPassword = "Encrypted",
            };
            var modelString = JsonConvert.SerializeObject(model);
            return modelString;
        }
    }
}
