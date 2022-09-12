using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Kajo.Models.Entities
{
    // Add profile data for application users by adding properties to the KajoUser class
    public class KajoUser : IdentityUser
    {
        public const int MaxFirstNameLength = 128;
        public const int MaxLastNameLength = 128;
        public const int MaxTeamLength = 128;
        public const int MaxAboutLength = 2048;

        [StringLength(MaxFirstNameLength)]
        public string FirstName { get; set; }

        [StringLength(MaxLastNameLength)]
        public string LastName { get; set; }

        [StringLength(MaxTeamLength)]
        public string Team { get; set; }

        [StringLength(MaxAboutLength)]
        public string About { get; set; }

    }
}
