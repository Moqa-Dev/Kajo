using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kajo.Models.Dtos.Identity
{
    public class LoginUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public override string ToString()
        {
            var model = new LoginUserDto { Username = this.Username, Password = "Encrypted" };
            //model.Password = "Encrypted";
            var modelString = JsonConvert.SerializeObject(model);
            return modelString;
        }
    }
}
