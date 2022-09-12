using Kajo.Models.Dtos.Base;
using Kajo.Models.Entities;
using Newtonsoft.Json;
using static Kajo.Infrastucture.Identity.Permissions;

namespace Kajo.Models.Dtos.Identity
{
    public class UpdateUserDto: IUpdateDto<KajoUser>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Team { get; set; }
        public string About { get; set; }
        public string PhoneNumber { get; set; }

        public void Update(KajoUser user)
        {
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;
            user.Team = this.Team;
            user.About = this.About;
            user.PhoneNumber = this.PhoneNumber;
        }

        public override string ToString()
        {
            var modelString = JsonConvert.SerializeObject(this);
            return modelString;
        }

    }
}
