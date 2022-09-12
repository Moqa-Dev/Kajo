using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kajo.Models.Dtos.Identity
{
    public class ResetPasswordDto
    {
        [Required]
        public string NewPassword { get; set; }

        public override string ToString()
        {
            var model = new ResetPasswordDto()
            {
                NewPassword = "Encrypted",
            };
            var modelString = JsonConvert.SerializeObject(model);
            return modelString;
        }
    }
}
