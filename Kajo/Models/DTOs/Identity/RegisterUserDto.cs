using Kajo.Models.Dtos.Base;
using Kajo.Models.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kajo.Models.Dtos.Identity
{
    public class RegisterUserDto : ICreateDto<KajoUser>
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Team { get; set; }
        public string About { get; set; }
        public string PhoneNumber { get; set; }

        public KajoUser ToEntity(string userID)
        {
            return new KajoUser
            {
                UserName = this.Username,
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Team = this.Team,
                About = this.About,
                PhoneNumber = this.PhoneNumber,
            };
        }

        public override string ToString()
        {
            var model = new RegisterUserDto
            {
                Username = this.Username,
                Password = "Encrypted",
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Team = this.Team,
                About = this.About,
                PhoneNumber = this.PhoneNumber,
            };
            var modelString = JsonConvert.SerializeObject(model);
            return modelString;
        }
    }
}
